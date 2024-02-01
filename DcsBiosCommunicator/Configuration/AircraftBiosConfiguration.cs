using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DcsBios.Communicator.Configuration
{
    /// <summary>
    /// A nested dictionary class containing all bios commands and their properties for a specific aircraft
    /// </summary>
    public class AircraftBiosConfiguration : Dictionary<string, BiosCategory>
    {
        public string AircraftName { get; private set; } = null!;

        public HashSet<string> Aliases { get; private set; } = null!;

        /// <summary>
        /// Parses all json files within the provided directories into <see cref="AircraftBiosConfiguration"/>s (basically
        /// just nested dictionaries).
        /// </summary>
        /// <remarks>
        /// This assumes that the directory contains a single aliases file <paramref name="configDirectory"/>. Skips all unparseable files and logs accordingly.
        /// </remarks>
        /// <param name="configDirectory">Name of the file containing the aliases, e.g. <code>AircraftAliases.json</code></param>
        /// <param name="logger">Logger</param>
        /// <param name="configLocations">Directories containing json configuration files outlining all available DCS-BIOS inputs/outputs</param>
        /// <returns>Parsed aircraft configurations</returns>
        public static async Task<AircraftBiosConfiguration[]> AllConfigurations(string? configDirectory = null, ILogger<AircraftBiosConfiguration>? logger = null,
            params string[] configLocations)
        {
            var allFiles = configLocations.SelectMany(GetAllJsonFilesBelowDirectory).ToList();
            var aliasTasks = allFiles.Where(f => f.Name == configDirectory).Select(f => GetAliases(f, logger));
            var aliasResults = await Task.WhenAll(aliasTasks);

            var aliases = new Dictionary<string, HashSet<string>>();

            foreach (var result in aliasResults)
            {
                foreach (var (key, value) in result)
                {
                    aliases.Add(key, value);
                }
            }

            var configTasks = configLocations
                .SelectMany(GetAllJsonFilesBelowDirectory)
                .Where(f => f.Name != configDirectory)
                .Select(f => BuildFromConfiguration(f, aliases, logger));

            return (await Task.WhenAll(configTasks)).Where(c => c != null).Select(c => c!).ToArray();
        }

        /// <summary>
        /// Creates a configuration from a single json file
        /// </summary>
        /// <param name="configFile">File to create a configuration from</param>
        /// <param name="allAliases">Potential naming aliases to consider: BIOS module -> [DCS code alias]</param>
        /// <param name="logger">Logger</param>
        /// <returns></returns>
        public static async Task<AircraftBiosConfiguration?> BuildFromConfiguration(FileSystemInfo configFile, Dictionary<string, HashSet<string>>? allAliases = null, ILogger<AircraftBiosConfiguration>? logger = null)
        {
            var fileData = await File.ReadAllTextAsync(configFile.FullName);

            try
            {
                var dcsConfiguration = JsonConvert.DeserializeObject<AircraftBiosConfiguration>(fileData,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy(),
                        }
                    });

                if (dcsConfiguration is null)
                {
                    throw new JsonSerializationException();
                }

                dcsConfiguration.AircraftName = Path.GetFileNameWithoutExtension(configFile.FullName);
                dcsConfiguration.Aliases =
                    allAliases != null && allAliases.TryGetValue(dcsConfiguration.AircraftName, out var aircraftAliases)
                        ? aircraftAliases
                        : new HashSet<string>();

                return dcsConfiguration;
            }
            catch (JsonSerializationException ex)
            {
                logger?.LogWarning("Error parsing config file {ConfigFile}, skipping...", configFile.FullName);
                logger?.LogDebug(ex, "exception:");
                return null;
            }
        }

        /// <summary>
        /// Returns all supported aliases.
        /// </summary>
        /// <param name="aliasFile">File where aliases are saved</param>
        /// <param name="logger"></param>
        /// <returns>BIOS module -> [DCS code]</returns>
        private static async Task<Dictionary<string, HashSet<string>>> GetAliases(FileSystemInfo aliasFile, ILogger<AircraftBiosConfiguration>? logger = null)
        {
            var fileData = await File.ReadAllTextAsync(aliasFile.FullName);
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, HashSet<string>>>(fileData) ??
                           new Dictionary<string, HashSet<string>>();

                var r = dict.SelectMany(kvp => kvp.Value.Select(module => (module, kvp.Key))).GroupBy(t => t.module)
                    .ToDictionary(g => g.Key, g2 => g2.Select(t => t.Key).ToHashSet());
                return r;
            }
            catch (JsonSerializationException ex)
            {
                logger?.LogWarning("Error parsing alias file {AliasFile}, skipping...", aliasFile.FullName);
                logger?.LogDebug(ex, "exception:");
                return new Dictionary<string, HashSet<string>>();
            }
        }

        private static IEnumerable<FileInfo> GetAllJsonFilesBelowDirectory(string directory)
        {
            var finalPath = FullOrRelativePath(Environment.ExpandEnvironmentVariables(directory));
            return Directory.Exists(finalPath)
                ? new DirectoryInfo(finalPath).EnumerateFiles("*.json", SearchOption.AllDirectories)
                : Enumerable.Empty<FileInfo>();
        }

        private static string FullOrRelativePath(string path)
        {
            return Path.IsPathFullyQualified(path) ? path : Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}
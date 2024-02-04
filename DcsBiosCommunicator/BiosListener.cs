using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DcsBios.Communicator.Configuration;
using DcsBios.Communicator.DataParsers;
using Microsoft.Extensions.Logging;

namespace DcsBios.Communicator
{
    public class BiosListener : IDisposable
    {
        public const string AircraftNameBiosCode = "_ACFT_NAME";

        private readonly BiosStateMachine _parser;
        private readonly IUdpReceiveClient _client;
        private readonly Dictionary<int, IntegerHandler> _integerActions = new();
        private readonly Dictionary<int, IList<StringParser>> _stringActions = new();
        private readonly Dictionary<string, Dictionary<int, IntegerHandler>> _moduleIntegerActions = new();
        private readonly Dictionary<string, Dictionary<int, StringParser>> _moduleStringActions = new();
        private string? _activeAircraft;
        private Task? _delegateThread;

        private readonly CancellationTokenSource _cts = new();

        private readonly IBiosTranslator _biosTranslator;
        private readonly ILogger<BiosListener> _log;

        public BiosListener(in IUdpReceiveClient client, in IBiosTranslator biosTranslator, in ILogger<BiosListener> logger)
        {
            _client = client;
            _biosTranslator = biosTranslator;
            _log = logger;
            _parser = new BiosStateMachine(logger);
            _parser.OnDataWrite += OnBiosDataReceived;
        }

        public void RegisterConfiguration(AircraftBiosConfiguration configuration)
        {
            _log.LogDebug("Loading config for module {Module}", configuration.AircraftName);
            foreach (var control in configuration.Values.SelectMany(c => c.Values))
            {
                RegisterControl(configuration.AircraftName, control);
            }
        }

        private void RegisterControl(string module, BiosControl control)
        {
            foreach (var output in control.Outputs)
            {
                _log.LogTrace("Registering control {Control} at {Address}", control.Identifier, output.Address);

                switch (output)
                {
                    case OutputInteger io:
                        RegisterIntegerControl(module, control, io);
                        break;
                    case OutputString so:
                        RegisterStringControl(module, control, so);
                        break;
                    default:
                        _log.LogCritical("invalid control output type {OutputType}", output.GetType());
                        throw new ArgumentException($"invalid output type: {output.GetType()}");
                }
            }
        }

        private void RegisterIntegerControl(string module, BiosControl control, OutputInteger output)
        {
            var newParser = new IntegerParser(output.Mask, output.ShiftBy, control.Identifier);
            if (!_moduleIntegerActions.ContainsKey(module))
                _moduleIntegerActions[module] = new Dictionary<int, IntegerHandler>();
            if (_moduleIntegerActions[module].TryGetValue(output.Address, out var moduleHandler))
            {
                moduleHandler.MaskShifts.Add(newParser);
            }
            else
            {
                RegisterIntegerAddress(module, new IntegerHandler(output.Address, new[] { newParser }));
            }

            if (_integerActions.TryGetValue(output.Address, out var handler))
            {
                handler.MaskShifts.Add(newParser);
            }
            else
            {
                RegisterIntegerAddress(new IntegerHandler(output.Address, new[] { newParser }));
            }
        }

        private void RegisterStringControl(string module, BiosControl control, OutputString output)
        {
            RegisterStringAddress(module, new StringParser(output.Address, output.MaxLength, control.Identifier));
        }

        private void RegisterIntegerAddress(IntegerHandler handler)
        {
            _integerActions[handler.Address] = handler;
        }

        private void RegisterIntegerAddress(string module, IntegerHandler handler)
        {
            if (!_moduleIntegerActions.ContainsKey(module))
                _moduleIntegerActions[module] = new Dictionary<int, IntegerHandler>();
            _moduleIntegerActions[module][handler.Address] = handler;
        }

        private void RegisterStringAddress(string module, StringParser parser)
        {
            if (!_moduleStringActions.TryGetValue(module, out var moduleParsers))
                moduleParsers = new Dictionary<int, StringParser>();
            for (var i = 0; i < parser.Length; i++)
            {
                moduleParsers.Add(parser.Address + i, parser);

                // the master list can have more than one item for a given module. Frankly if we ever have to use this we're probably
                // in some deep doo-doo, but it's better to be defensive given https://github.com/charliefoxtwo/TouchDCS/issues/18
                if (!_stringActions.TryGetValue(parser.Address + i, out var parsers)) parsers = new List<StringParser>();
                parsers.Add(parser);
                _stringActions[parser.Address + i] = parsers;
            }

            _moduleStringActions[module] = moduleParsers;
        }

        private void OnBiosDataReceived(int address, int data)
        {
            _log.LogTrace("{Address:x4} -> got data -> {Data:x4}", address, data);

            if (_activeAircraft is not null &&
                _moduleIntegerActions.TryGetValue(_activeAircraft, out var integerActions) &&
                integerActions.TryGetValue(address, out var handler) ||
                _integerActions.TryGetValue(address, out handler))
            {
                _log.LogTrace("{Address:x4} -> got int data -> {Data:x4}", address, data);
                foreach (var mask in handler.MaskShifts)
                {
                    mask.AddData(address, data);
                    _biosTranslator.FromBios(mask.BiosCode, mask.CurrentValue);
                }
            }

            // some controls are registered to both strings and integers, because life is fun like that.
            if (_activeAircraft is not null &&
                _moduleStringActions.TryGetValue(_activeAircraft, out var stringActions) &&
                stringActions.TryGetValue(address, out var parser))
            {
                ProcessStringData(parser, address, data);
            }
            else if (_stringActions.TryGetValue(address, out var parsers))
            {
                foreach (var p in parsers)
                {
                    ProcessStringData(p, address, data);
                }
            }
        }

        private void ProcessStringData(StringParser parser, int address, int data)
        {
            _log.LogTrace("{Address:x4} -> got string data -> {Data:x4}", address, data);

            parser.AddData(address, data);

            var result = parser.CurrentValue;
            if (parser.BiosCode == AircraftNameBiosCode)
            {
                if (!parser.DataReady) return;
                // name is fixed-length and null-terminated. fun.
                result = result.Split(default(char))[0];
                if (string.IsNullOrEmpty(result)) return; // we just haven't loaded the aircraft name yet
                if (_activeAircraft != result)
                {
                    _activeAircraft = result;
                    _log.LogInformation("New aircraft detected -> {{{ActiveAircraft}}}", _activeAircraft);
                }
            }

            _biosTranslator.FromBios(parser.BiosCode, result);
        }

        public void Start()
        {
            _log.LogDebug("Starting DCS-BIOS listener...");

            if (_delegateThread is not null)
            {
                _log.LogWarning("DCS-BIOS listener thread already started");
            }
            else if (_cts.IsCancellationRequested)
            {
                _log.LogWarning("DCS-BIOS listener cancellation requested, aborting start...");
            }
            else
            {
                _delegateThread = Listener(_cts.Token);
            }

            _log.LogInformation("DCS-BIOS listener started");
        }

        /// <summary>
        /// Stops the existing listener thread and enacts a blacking wait until it is complete.
        /// </summary>
        public void Stop()
        {
            _log.LogDebug("Stopping DCS-BIOS listener...");
            _cts.Cancel();
            //_delegateThread?.Wait();
            _delegateThread?.Wait(500);
            _log.LogInformation("DCS-BIOS listener stopped");
        }

        private async Task Listener(CancellationToken ctx)
        {
            while (!ctx.IsCancellationRequested)
            {
                try
                {
                    _log.LogTrace("awaiting bios data...");
                    var data = await _client.ReceiveAsync();
                    _log.LogTrace("bios data received of length {DataLength}", data.Buffer.Length);
                    _parser.ProcessBytes(data.Buffer);
                }
                catch (Exception ex)
                {
                    _log.LogCritical("Critical exception in Bios Listener: {Exception}", ex);
                    throw;
                }
            }

            _log.LogWarning("Stopping DCS-BIOS Listener...");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Stop();
            _delegateThread?.Dispose();
            _cts.Dispose();
        }
    }
}

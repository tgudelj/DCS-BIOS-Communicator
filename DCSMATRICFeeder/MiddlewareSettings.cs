namespace DCSMATRICFeeder {
    /// <summary>
    /// Represents DCS-BIOS MATRIC middleware configuration
    /// </summary>
    internal class MiddlewareSettings {
        public MiddlewareSettings() {
            ListenAddress = "239.255.50.10";
            ListenPort = 5010;
            DCSBIOSJsonPath = "";
            AircraftVariables = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// IP address to listen on for DCS-BIOS, default is multicast address 239.255.50.10
        /// </summary>
        public string ListenAddress {  get; set; }

        /// <summary>
        /// Port to listen on for DCS-BIOS, default is 5010
        /// </summary>
        public int ListenPort { get; set; }

        /// <summary>
        /// Path to DCS-BIOS doc/json directory, default is "%userprofile%/Saved Games/DCS.openbeta/Scripts/DCS-BIOS/doc/json/"
        /// </summary>
        public string DCSBIOSJsonPath {  get; set; }

        /// <summary>
        /// Chosen variables list per aircraft
        /// key - aircraft name
        /// value - list of variables to forward to MATRIC
        /// </summary>
        public Dictionary<string, List<string>> AircraftVariables { get; set; }
    }
}

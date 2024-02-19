using DcsBios.Communicator;
using Matric.Integration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace DCSMATRICFeeder {
    internal class MatricDCSTranslator : IBiosTranslator, IDisposable {

        internal class TxRxNotificationEventArgs : EventArgs {
            public TxRxNotificationEventArgs(int count) {
                Count = count;
            }
            public int Count { get; set; }
        }

        int MAX_VAR_LIST = 100;
        const string COMMON_DATA = "CommonData";
        const string METADATA_START = "MetadataStart";
        const string METADATA_END = "MetadataEnd";
        const string DCS_INPUT_COMMAND = "DCS_INPUT_COMMAND";
        string _currentAircraftName = string.Empty;
        List<string> _allowedVariables = new List<string>();
        Matric.Integration.Matric matricComm;
        Dictionary<string, object> _dcsValues;
        Dictionary<string, ServerVariable> _changesBuffer;
        System.Threading.Timer _timer;
        BiosUdpClient biosClient = null;
        ILogger _logger;
        private object locker = new object();
        public MatricDCSTranslator(int matricIntegrationPort, BiosUdpClient biosClient, ILogger logger) {
            _logger = logger;
            _dcsValues = new Dictionary<string, object>();
            _changesBuffer = new Dictionary<string, ServerVariable>();
            _timer = new System.Threading.Timer(SendUpdates,
                                                null,
                                                100,
                                                (int)Math.Round(1000d / Program.mwSettings.UpdateFrequency)
                                                );            
            matricComm = new Matric.Integration.Matric("DCS", null, matricIntegrationPort);
            matricComm.OnVariablesChanged += MatricComm_OnVariablesChanged;
            matricComm.OnControlInteraction += MatricComm_OnControlInteraction;
            //create DCS_INPUT_COMMAND as user editable variable
            ServerVariable dcsInputVariable = new ServerVariable() {
                Name = DCS_INPUT_COMMAND,
                Value = "",
                VariableType = ServerVariable.ServerVariableType.STRING,
                IsPersistent = true,
                IsUserEditable = true
            };
            _changesBuffer.Add(dcsInputVariable.Name, dcsInputVariable);
        }



        public event EventHandler<TxRxNotificationEventArgs> UpdateSentNotification;

        public event EventHandler<TxRxNotificationEventArgs> UpdateBufferSizeNotification;

        private void MatricComm_OnVariablesChanged(object sender, ServerVariablesChangedEventArgs data) {
            //throw new NotImplementedException();
            Debug.WriteLine("Got variables changed event notification");
            if (data.ChangedVariables.Contains(DCS_INPUT_COMMAND)) { 
                string command = data.Variables[DCS_INPUT_COMMAND].Value.ToString();
                biosClient?.Send(command, string.Empty); //method takes separate biosAddress and data, but in the end it is concatenated and sent via UDP anyway
            }
        }

        private void MatricComm_OnControlInteraction(object sender, object data) {
            //throw new NotImplementedException();
            Debug.WriteLine("Got control interaction event notification " + data.ToString() );
        }

        public void FromBios<T>(string biosCode, T data) {
            if (biosCode == "_ACFT_NAME") {
                //get list of user selected variables. If it doesn't exist do not filter, forward everything to MATRIC
                if (!data.ToString().Equals(_currentAircraftName)) { 
                    //New aircraft, load config for aircraft
                    _currentAircraftName = data.ToString();
                    if (Program.mwSettings.AircraftVariables.ContainsKey(_currentAircraftName)) {
                        _allowedVariables.Clear();
                        _allowedVariables = Program.mwSettings.AircraftVariables[_currentAircraftName];
                        //Add common and metadata
                        if (Program.mwSettings.AircraftVariables.ContainsKey(COMMON_DATA)) {
                            _allowedVariables.AddRange(Program.mwSettings.AircraftVariables[COMMON_DATA]);
                        }
                        if (Program.mwSettings.AircraftVariables.ContainsKey(METADATA_START)) {
                            _allowedVariables.AddRange(Program.mwSettings.AircraftVariables[METADATA_START]);
                        }
                        if (Program.mwSettings.AircraftVariables.ContainsKey(METADATA_END)) {
                            _allowedVariables.AddRange(Program.mwSettings.AircraftVariables[METADATA_END]);
                        }
                    }
                }
            }

            if(_allowedVariables.Count > 0) {
                if(!_allowedVariables.Contains(biosCode)) {
                    return;
                }
            }

            string varName = $"dcs_{biosCode}";
            lock(locker) {
                object currentData = null;
                if (_dcsValues.TryGetValue(varName, out currentData)) {
                    if (currentData.ToString() != data.ToString()) {
                        //add or replace in changes
                        if (_changesBuffer.ContainsKey(varName)) {
                            _changesBuffer[varName].Value = data;
                        }
                        else {
                            if (data is string) {
                                _changesBuffer.Add(varName, new ServerVariable() { Name = varName, Value = data.ToString(), VariableType = ServerVariable.ServerVariableType.STRING });
                            }
                            else {
                                //int 
                                _changesBuffer.Add(varName, new ServerVariable() { Name = varName, Value = data, VariableType = ServerVariable.ServerVariableType.NUMBER });
                            }
                        }
                    }
                }
                else {
                    //add or replace in changes
                    if (_changesBuffer.ContainsKey(varName)) {
                        _changesBuffer[varName].Value = data;
                    }
                    else {
                        if (data is string) {
                            _changesBuffer.Add(varName, new ServerVariable() { Name = varName, Value = data.ToString(), VariableType = ServerVariable.ServerVariableType.STRING });
                        }
                        else {
                            //int 
                            _changesBuffer.Add(varName, new ServerVariable() { Name = varName, Value = data, VariableType = ServerVariable.ServerVariableType.NUMBER });
                        }
                    }
                }

            }
        }

        public void SendUpdates(object state) {
            Debug.WriteLine($"Changes: {_changesBuffer.Keys.Count}");
            int bufferSize = _changesBuffer.Count;
            Task.Run(() => {
                UpdateBufferSizeNotification?.Invoke(this, new TxRxNotificationEventArgs(Math.Min(bufferSize, 200)));
            });
            int sent = 0;
            lock(locker) {
                if (_changesBuffer.Count >= MAX_VAR_LIST) {
                    Dictionary<string, ServerVariable> sendBuffer = _changesBuffer.Take(MAX_VAR_LIST).ToDictionary();
                    foreach (string key in sendBuffer.Keys) {
                        _changesBuffer.Remove(key);
                    }
                    matricComm.SetVariables(sendBuffer.Values.ToList<ServerVariable>());
                    sent = sendBuffer.Count;
                }
                else { 
                    matricComm.SetVariables(_changesBuffer.Values.ToList<ServerVariable>());
                    sent = _changesBuffer.Count;
                    _changesBuffer.Clear();            
                }
            }
            Task.Run(() => UpdateSentNotification?.Invoke(this, new TxRxNotificationEventArgs(Math.Min(sent, 100))));
        }

        public void Dispose() {
            _timer.Dispose();
            if(matricComm != null) {
                matricComm.Stop();
                matricComm.Dispose();
            }
        }
    }
}

using DcsBios.Communicator;
using Matric.Integration;
using Microsoft.Extensions.Logging;
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
        Matric.Integration.Matric matricComm;
        Dictionary<string, object> _dcsValues;
        Dictionary<string, ServerVariable> _changesBuffer;
        System.Threading.Timer _timer;
        ILogger _logger;
        private object locker = new object();
        public MatricDCSTranslator(int matricIntegrationPort, ILogger logger) {
            _logger = logger;
            _dcsValues = new Dictionary<string, object>();
            _changesBuffer = new Dictionary<string, ServerVariable>();
            _timer = new System.Threading.Timer(SendUpdates, null, 100, 200);            
            matricComm = new Matric.Integration.Matric("DCS", null, matricIntegrationPort);
            matricComm.OnVariablesChanged += MatricComm_OnVariablesChanged;
        }

        public event EventHandler<TxRxNotificationEventArgs> UpdateSentNotification;

        public event EventHandler<TxRxNotificationEventArgs> UpdateBufferSizeNotification;

        private void MatricComm_OnVariablesChanged(object sender, ServerVariablesChangedEventArgs data) {
            //throw new NotImplementedException();
        }

        public void FromBios<T>(string biosCode, T data) {
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

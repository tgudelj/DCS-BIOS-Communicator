using DcsBios.Communicator;
using Matric.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MatricDCSCommunicator {
    internal class MatricDCSTranslator : IBiosTranslator {
        Matric.Integration.Matric matricComm;
        Dictionary<string, object> _dcsValues;
        Dictionary<string, ServerVariable> _changesBuffer;
        Timer _timer;
        public MatricDCSTranslator() {
            _dcsValues = new Dictionary<string, object>();
            _changesBuffer = new Dictionary<string, ServerVariable>();
            _timer = new Timer(SendUpdates, null, 100, 100);            
            matricComm = new Matric.Integration.Matric("DCS", null, 5300);
            matricComm.OnVariablesChanged += MatricComm_OnVariablesChanged;
        }

        private void MatricComm_OnVariablesChanged(object sender, ServerVariablesChangedEventArgs data) {
            //throw new NotImplementedException();
        }

        public void FromBios<T>(string biosCode, T data) {
            //Console.WriteLine($"{biosCode}     {data}");
            object currentData = null;
            if (_dcsValues.TryGetValue(biosCode, out currentData)) {
                if (currentData.ToString() != data.ToString()) {
                    //add or replace in changes
                    if (_changesBuffer.ContainsKey(biosCode)) {
                        _changesBuffer[biosCode].Value = data;
                    }
                    else {
                        if (data is string) {
                            _changesBuffer.Add(biosCode, new ServerVariable() { Name = biosCode, Value = data.ToString(), VariableType = ServerVariable.ServerVariableType.STRING });
                        }
                        else {
                            //int 
                            _changesBuffer.Add(biosCode, new ServerVariable() { Name = biosCode, Value = data, VariableType = ServerVariable.ServerVariableType.NUMBER });
                        }
                    }
                }
            }
            else {
                //add or replace in changes
                if (_changesBuffer.ContainsKey(biosCode)) {
                    _changesBuffer[biosCode].Value = data;
                }
                else {
                    if (data is string) {
                        _changesBuffer.Add(biosCode, new ServerVariable() { Name = biosCode, Value = data.ToString(), VariableType = ServerVariable.ServerVariableType.STRING });
                    }
                    else {
                        //int 
                        _changesBuffer.Add(biosCode, new ServerVariable() { Name = biosCode, Value = data, VariableType = ServerVariable.ServerVariableType.NUMBER });
                    }
                }
            }
        }

        public void SendUpdates(object state) {
            Console.WriteLine($"Changes: {_changesBuffer.Keys.Count}");
            matricComm.SetVariables(_changesBuffer.Values.ToList<ServerVariable>());
            _changesBuffer.Clear();
        }
    }
}

using DcsBios.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricDCSCommunicator {
    internal class MatricDCSTranslator : IBiosTranslator {
        public void FromBios<T>(string biosCode, T data) {
            Console.WriteLine($"{biosCode}     {data}");
        }
    }
}

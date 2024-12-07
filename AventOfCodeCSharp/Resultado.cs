using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Resultado
    {
        public Resultado()
        {
            Test = new long[2];
            Input = new long[2];
        }
        public long[] Test { get; set; }
        public long[] Input { get; set; }        
    }
}

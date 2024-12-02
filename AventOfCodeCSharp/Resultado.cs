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
            Test = new int[2];
            Input = new int[2];
        }
        public int[] Test { get; set; }
        public int[] Input { get; set; }        
    }
}

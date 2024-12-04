using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Point
    {        
        public Point(int row, int column, char value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public char? Value { get; set; }
        
    }
}

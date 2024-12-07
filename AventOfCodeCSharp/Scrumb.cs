using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public  class Scrumb : Point
    {
        public DirectionType Direction { get; set; }
        public Scrumb(int row, int column, char value, DirectionType direction) : base(row, column, value)
        {
            Direction = direction;            
        }
        public bool IsEqual(Scrumb scrumb)
        {
            return Row == scrumb.Row && Column == scrumb.Column && Direction == scrumb.Direction;
        }
    }
}

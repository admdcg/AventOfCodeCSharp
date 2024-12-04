using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Line
    {
        public Line(Point point1, Point point2)
        {            
            Point1 = point1;
            Point2 = point2;
        }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public string Value { get; set; }
    }
}

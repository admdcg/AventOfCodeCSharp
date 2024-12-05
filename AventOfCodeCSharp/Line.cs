using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Line
    {
        public Line() { }
        public Line(Point point1, Point point2)
        {            
            Point1 = point1;
            Point2 = point2;            
        }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public string Value 
        { 
            get
            {
                return Points.Aggregate("", (acc, p) => acc + p.Value);
            }
        }
        public List<Point> Points { get; set; } = new List<Point>();
        public void AddPoint(Point point)
        {
            Points.Add(point);
            Point1 = Points.First();
            Point2 = Points.Last();
        }
    }
}

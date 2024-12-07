using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Geometry.Euclidean;

namespace AventOfCodeCSharp
{
    public class Point
    {
        public Point(int row, int column)
        {
            Row = row;
            Column = column;
            Value = null;
        }
        public Point(int row, int column, char value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public char? Value { get; set; }        
        public ConsoleColor ForegroundColor { get; set; } = Console.ForegroundColor;
        public ConsoleColor BackgroundColor { get; set; } = Console.BackgroundColor;
        public bool IsEqual(Point point)
        {
            return Row == point.Row && Column == point.Column;
        }
        // Sobrecarga del operador +
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.Row + p2.Row, p1.Column + p2.Column);
        }
        // Sobrecarga del operador ++
        public static Point operator ++(Point p)
        {
            return new Point(p.Row + 1, p.Column + 1);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.Row - p2.Row, p1.Column - p2.Column);
        }
        // Sobrecarga del operador ++
        public static Point operator --(Point p)
        {
            return new Point(p.Row - 1, p.Column - 1);
        }

        // Sobrecarga del método ToString para facilitar la visualización
        public override string ToString()
        {
            return $"({Row},{Column}): {Value}";
        }
        public static double Distance(Point p1, Point p2)
        {
            var dRow = p2.Row - p1.Row;
            var dCol = p2.Column - p2.Column;
            return Math.Sqrt(dRow * dRow + dCol * dCol);
        }

    }
}

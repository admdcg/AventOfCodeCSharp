using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    internal class Helper
    {
        public static List<Line> GetReverseLines(MapText mapText, List<Line> rectas)
        {
            var rectasInversas = new List<Line>();
            foreach (var recta in rectas)
            {
                var rectaInv = mapText.ReverseLine(recta);
                rectasInversas.Add(rectaInv);
            }
            return rectasInversas;
        }
        public static void Print2Puntos(Point point1, Point point2)
        {
            Console.WriteLine($"({point1.Row},{point1.Column})({point2.Row},{point2.Column})");
        }
        public static void PrintPunto(Point point)
        {
            Console.WriteLine($"({point.Row},{point.Column})");
        }
        public static void PrintRectas(String header, List<Line> rectas)
        {
            Console.WriteLine(header);
            foreach (var recta in rectas)
            {
                Console.WriteLine(recta.Value);
            }
        }
        public static void PrintList<T>(IEnumerable<T> lst)
        {
            Console.WriteLine(string.Join(", ", lst));
        }
    }
}

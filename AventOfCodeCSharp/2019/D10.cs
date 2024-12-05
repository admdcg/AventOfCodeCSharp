using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;


namespace AdventOfCodeCSharp.Y2019
{
    public partial class Program : Solutions
    {
        public static void Dia10(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia10_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia10_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia10_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            filePath = Path.Combine(AppContext.BaseDirectory, year.ToString(), "inputs", $"dia10-A.txt");
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;
            var rectas = new List<Line>();
            var point1 = mapText.GetPoint(0, 3);
            var point2 = mapText.GetPoint(6, 3);
            var line = mapText.GetLine(point1, point2);
            mapText.MarkLine(line, ConsoleColor.White, ConsoleColor.Green);
            mapText.Print();
            Console.WriteLine(line.Value);

            var rectasAngulos = mapText.GetAngleLines(mapText.GetPoint(5,5));
            PrintRectas("Anguladas: ", rectasAngulos);
            
            

            //foreach (var recta in rectas)
            //{
            //    foreach (Match m in regex.Matches(recta.Value))
            //    {
            //        totalSum += 1;
            //    }
            //}
            Summary(year, dia, parte, test, totalSum);
        }
        public static void Dia10_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;

            for (int f = 0; f < mapText.Height; f++)
            {
                var line = mapText.Lines[f];
                for (int c = 0; c < line.Length; c++)
                {
                    var equis = new List<Line>();
                    var point1x = new Point(f - 1, c - 1);
                    if (mapText.IsInBounds(point1x))
                    {
                        var point2x = new Point(f + 1, c + 1);
                        if (mapText.IsInBounds(point2x))
                        {
                            var recta = mapText.GetLine(point1x, point2x);
                            equis.Add(recta);
                            var rectaInv = mapText.ReverseLine(recta);
                            equis.Add(rectaInv);
                        }
                    }
                    else
                    {
                        continue;
                    }
                    var point1y = new Point(f + 1, c - 1);
                    if (mapText.IsInBounds(point1y))
                    {
                        var point2y = new Point(f - 1, c + 1);
                        if (mapText.IsInBounds(point2y))
                        {
                            var recta = mapText.GetLine(point1y, point2y);
                            equis.Add(recta);
                            var rectaInv = mapText.ReverseLine(recta);
                            equis.Add(rectaInv);
                        }
                    }
                    if (equis.Count(l => l.Value == "MAS") == 2)
                    {
                        totalSum += 1;
                    }
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }
        private static List<Line> GetReverseLines(MapText mapText, List<Line> rectas)
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

            foreach (var c in lst)
            {
                Console.WriteLine((T)c);
            }
        }

    }
}



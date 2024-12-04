using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;


namespace AdventOfCodeCSharp.Y2024
{

    public partial class Program : Solutions
    {
        public static void Dia04(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia04_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia04_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia04_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;
            string XMAS = "XMAS";
            int xmasLength = XMAS.Length;

            var regex = new Regex(XMAS);
            var rectas = new List<Line>();
            var rectasFilas = new List<Line>();
            var rectasColumnas = new List<Line>();
            var rectasDiagonalesFilas = new List<Line>();
            var rectasDiagonalesColumnas = new List<Line>();
            var rectasDiagonalesFilasBack = new List<Line>();
            var rectasDiagonalesColumnasBack = new List<Line>();
            for (int f = 0; f < mapText.Height; f++)
            {
                var line = mapText.Lines[f];
                var point1 = mapText.GetPoint(f, 0);
                var point2 = mapText.GetPoint(f, line.Length - 1);
                var recta = mapText.GetLine(point1, point2);                
                rectasFilas.Add(recta);                
            }
            var rectasFilasInv = GetReverseLines(mapText, rectasFilas);
            for (int c = 0; c < mapText.Lines[0].Length; c++)
            {                
                var point1 = mapText.GetPoint(0, c);
                var point2 = mapText.GetPoint(mapText.Height - 1, c);
                var recta = mapText.GetLine(point1, point2);
                rectasColumnas.Add(recta);                
            }
            var rectasColumnasInv = GetReverseLines(mapText, rectasColumnas);
            //Diagonales normales
            for (int f = 0; f < mapText.Height - xmasLength + 1; f++)
            {                
                var point1 = mapText.GetPoint(f, 0);
                int row = mapText.Height - 1;
                var line = mapText.Lines[row];
                int col = (line.Length - 1) - f;
                var point2 = mapText.GetPoint(row, col);
                var recta = mapText.GetLine(point1, point2);                
                rectasDiagonalesFilas.Add(recta);
            }
            var rectasDiagonalesFilasInv = GetReverseLines(mapText, rectasDiagonalesFilas);
            for (int c = 1; c < mapText.Lines[0].Length - xmasLength + 1; c++)
            {                
                var point1 = mapText.GetPoint(0, c);
                int row = (mapText.Height - 1) - c;
                var point2 = mapText.GetPoint(row, mapText.Lines[row].Length - 1);
                var recta = mapText.GetLine(point1, point2);
                rectasDiagonalesColumnas.Add(recta);
            }
            var rectasDiagonalesColumnnasInv = GetReverseLines(mapText, rectasDiagonalesColumnas);
            //Diagonales backslash
            for (int f = mapText.Height -1; f >= xmasLength - 1  ; f--)
            {
                var point1 = mapText.GetPoint(f, 0);
                int row = 0;
                var line = mapText.Lines[row];
                int col = (line.Length - 1) - (line.Length - 1 - f);
                var point2 = mapText.GetPoint(row, col);
                var recta = mapText.GetLine(point1, point2);
                rectasDiagonalesFilasBack.Add(recta);
            }
            var rectasDiagonalesFilasBackInv = GetReverseLines(mapText, rectasDiagonalesFilasBack);
            for (int c = 1; c < mapText.Lines[0].Length - xmasLength + 1; c++)
            {
                var point1 = mapText.GetPoint(mapText.Height - 1 , c);
                int row = c;
                var line = mapText.Lines[row];
                var col = line.Length - 1;
                var point2 = mapText.GetPoint(row, col);
                var recta = mapText.GetLine(point1, point2);
                rectasDiagonalesColumnasBack.Add(recta);
            }
            var rectasDiagonalesColumnnasBackInv = GetReverseLines(mapText, rectasDiagonalesColumnasBack);
            rectas.AddRange(rectasFilas);
            rectas.AddRange(rectasFilasInv);
            rectas.AddRange(rectasColumnas);
            rectas.AddRange(rectasColumnasInv);
            rectas.AddRange(rectasDiagonalesFilas);
            rectas.AddRange(rectasDiagonalesFilasInv);
            rectas.AddRange(rectasDiagonalesColumnas);
            rectas.AddRange(rectasDiagonalesColumnnasInv);
            rectas.AddRange(rectasDiagonalesFilasBack);
            rectas.AddRange(rectasDiagonalesFilasBackInv);
            rectas.AddRange(rectasDiagonalesColumnasBack);
            rectas.AddRange(rectasDiagonalesColumnnasBackInv);

            //PrintRectas("Filas: ", rectasFilas);
            //PrintRectas("Filas Inv: ", rectasFilasInv);
            //PrintRectas("Columnas: ", rectasColumnas);
            //PrintRectas("Columnas Inv: ", rectasColumnasInv);
            //PrintRectas("DiagonalesFilas: ", rectasDiagonalesFilas);
            //PrintRectas("DiagonalesFilas Inv: ", rectasDiagonalesFilasInv);
            //PrintRectas("DiagonalesColumnas: ", rectasDiagonalesColumnas);
            //PrintRectas("DiagonalesColumnas Inv: ", rectasDiagonalesColumnnasInv);
            //PrintRectas("DiagonalesFilasBack: ", rectasDiagonalesFilasBack);
            //PrintRectas("DiagonalesFilasBack Inv: ", rectasDiagonalesFilasBackInv);
            //PrintRectas("DiagonalesColumnasBack: ", rectasDiagonalesColumnasBack);
            //PrintRectas("DiagonalesColumnasBack Inv: ", rectasDiagonalesColumnnasBackInv);
            foreach (var recta in rectas)
            {
                foreach (Match m in regex.Matches(recta.Value))
                {
                    totalSum += 1;
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }        
        
        public static void Dia04_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;            
            
            for (int f = 0; f < mapText.Height; f++)
            {
                var line = mapText.Lines[f];
                for (int c=0; c < line.Length; c++)
                {
                    var Equis = new List<Line>();
                    var point1x = new Point(f-1, c-1);
                    if (mapText.IsInBounds(point1x))
                    {
                        var point2x = new Point(f + 1, c + 1);
                        if (mapText.IsInBounds(point2x))
                        {
                            var recta = mapText.GetLine(point1x, point2x);
                            Equis.Add(recta);
                            var rectaInv = mapText.ReverseLine(recta);
                            Equis.Add(rectaInv);
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
                            Equis.Add(recta);
                            var rectaInv = mapText.ReverseLine(recta);
                            Equis.Add(rectaInv);
                        }                        
                    }
                    if (Equis.Count(l => l.Value == "MAS") == 2)
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
            foreach(var recta in rectas)
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



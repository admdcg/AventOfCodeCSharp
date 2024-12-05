using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;


namespace AdventOfCodeCSharp.Y2023
{

    public partial class Program : Solutions
    {
        public static void Dia03(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia03_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia03_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia03_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;

            var regex = new Regex("\\d+");
            for(int f= 0; f<lines.Count();f++)
            {
                var line = lines[f];
                foreach (Match m in regex.Matches(line))
                {                    
                    var adyacentes = mapText.Adjacents(f, m.Index, m.Value.Length);
                    if (adyacentes.All(p => p.Value == '.'))
                    {
                        Console.WriteLine($"{m.Value} Incorrecto:");
                        //PrintList(adyacentes);
                        Console.WriteLine();
                    }
                    else
                    {
                        var num = int.Parse(m.Value);
                        totalSum += num;
                        var point = adyacentes.FirstOrDefault(p => p.Value != '.');
                        Console.WriteLine($"{m.Value} Correcto. Hay un '{point.Value}' en la ({point.Row}, {point.Column})");
                    }
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }
        public static void Dia03_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var mapText = new MapText(lines);
            int totalSum = 0;
            var regexDigit = new Regex("\\d+");
            var regex = new Regex("\\*");
            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                foreach (Match m in regex.Matches(line))
                {
                    var point = mapText.GetPoint(f, m.Index);
                    var rectas = mapText.GetLinesWhileNotEmpty(point);
                    Console.ResetColor();
                    Console.WriteLine($"Recta adjacentes no vacías para ({point.Row}, {point.Column}): '{point.Value}': ");
                    var numbers = new List<int>();
                    foreach (var recta in rectas)
                    {
                        foreach (Match matchDigit in regexDigit.Matches(recta.Value))
                        {
                            var num = int.Parse(matchDigit.Value);
                            numbers.Add(num);
                        }
                        Console.WriteLine($"{recta.Value}");
                    }
                    if (numbers.Count > 1)
                    {
                        var sum = numbers[0] * numbers[1];
                        totalSum += sum;
                        Console.WriteLine($"La suma de {numbers[0]} * {numbers[1]} = {sum}");

                    }
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }
        
        public static void PrintList<T>(IEnumerable<T> lst)
        {
            foreach (var c in lst)
            {
                Console.WriteLine((T)c);
            }
        }
        
        public static List<string> PalabrasAdyacentesArriba(List<string> lines, int row, int col)
        {
            var adyacentes = new List<string>();
            int rows = lines.Count();
            int cols = lines[0].Count();

            for (int c = -1; c <= 1; c++) // Columna adyacente (arriba, misma fila, abajo)
            {
                int r = -1; // Fila de arriba
                int newRow = row + r;
                int newCol = col + c;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    //adyacentes.Add(lines[newRow][newCol]);
                }
            }
            return adyacentes;
        }        
    }
}


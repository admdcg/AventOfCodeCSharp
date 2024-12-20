﻿using Microsoft.VisualBasic;
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
            var rectasFilas = mapText.GetRowLines();
            var rectasColumnas = mapText.GetColumnLines();
            var rectasDiagonales = mapText.GetDiagonals();
            
            rectas.AddRange(rectasFilas);
            rectas.AddRange(rectasColumnas);
            rectas.AddRange(rectasDiagonales);
            
            //PrintRectas("Filas: ", rectasFilas);
            //Console.WriteLine("Columna5:" + mapText.Points.Where(p => Enumerable.Range(3,5).Contains(p.Column)).Aggregate("", (acc, p) => acc + p.Value));
            //PrintRectas("Columnas: ", rectasColumnas);
            Helper.PrintRectas("Diagonales: ", rectasDiagonales);
            var rectasInvretidas = Helper.GetReverseLines(mapText, rectas);
            rectas.AddRange(rectasInvretidas);
            
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
                    var equis = new List<Line>();
                    var point1x = new Point(f-1, c-1);
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
    }
}



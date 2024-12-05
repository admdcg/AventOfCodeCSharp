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
        public static void Dia05(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia05_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia05_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia05_1(int year, int dia, int parte, bool test, bool other2Test = false)
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
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }
        public static void Dia05_2(int year, int dia, int parte, bool test, bool other2Test = false)
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
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }
    }
}



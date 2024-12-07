using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Collections;


namespace AdventOfCodeCSharp.Y2024
{

    public partial class Program : Solutions
    {
        public static void Dia08(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia08_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia08_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia08_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));            
            long totalSum = 0;

            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                var numeros = StringHelper.SplitNumbers<long>(lines[f]);
                var solucion = numeros[0];
                
            }            
            Summary(year, dia, parte, test, totalSum);
        }
        public static void Dia08_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            long totalSum = 0;

            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                var numeros = StringHelper.SplitNumbers<long>(lines[f]);
                var solucion = numeros[0];

            }
            Summary(year, dia, parte, test, totalSum);
        }        
    }
}



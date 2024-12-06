using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace AdventOfCodeCSharp.Y2024
{

    public partial class Program
    {
        public static void Dia03_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);

            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            //List<string> lines = new List<string>(File.ReadAllLines("2023\\inputs\\archivo.txt"));
            int totalSum = 0;            
            var regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            var regexBetweenDoDont = new Regex("do\\(\\).*?don't\\(\\)");
            var regexDont = new Regex("don't\\(\\)");
            var newLines = new List<string>();
            int pos = 0;
            String newLine = "";
            var sb = new StringBuilder();
            foreach(var l in lines)
            {
                sb.Append(l);
            }
            var line = sb.ToString();
            
            var matchs = regexDont.Matches(line);
            if (matchs.Count > 0)
            {
                Match mDont = matchs[0];
                pos = mDont.Index;
                newLine = line.Substring(0, pos + 7);
            }
            else
            {
                pos = line.Length-1;
                newLine = line.Substring(0);
            }                
            newLines.Add(newLine);
            
            var matches = regexBetweenDoDont.Matches(line, pos + 7);
            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    newLines.Add(m.Value);
                }
            }
            for (int f = 0; f < newLines.Count(); f++)
            {
                line = newLines[f];
                matches = regex.Matches(line);
                foreach (Match m in matches)
                {
                    if (m.Success)
                    {
                        //var adyacentes = Adyacentes(lines, f, m.Index, m.Value.Count());
                        Console.WriteLine($"Capturado: {m.Value}");
                        int intValue1 = 0;
                        int intValue2 = 0;
                        if (int.TryParse(m.Groups[1].Value, out intValue1))
                        {
                            if (int.TryParse(m.Groups[2].Value, out intValue2))
                            {
                                Console.WriteLine($"Valores: {intValue1} y {intValue2}");
                                totalSum += intValue1 * intValue2;

                            }
                        }
                    }
                }
            }

            Summary(year, dia, parte, test, totalSum);

        }
        public static void Dia03_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);

            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            //List<string> lines = new List<string>(File.ReadAllLines("2023\\inputs\\archivo.txt"));
            int totalSum = 0;

            var regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                foreach (Match m in regex.Matches(line))
                {
                    if (m.Success)
                    {
                        //var adyacentes = Adyacentes(lines, f, m.Index, m.Value.Count());
                        Console.WriteLine($"Capturado: {m.Value}");
                        int intValue1 = 0;
                        int intValue2 = 0;
                        if (int.TryParse(m.Groups[1].Value, out intValue1))
                        {
                            if (int.TryParse(m.Groups[2].Value, out intValue2))
                            {
                                Console.WriteLine($"Valores: {intValue1} y {intValue2}");
                                totalSum += intValue1 * intValue2;

                            }
                        }
                    }                    
                }
            }

            Summary(year, dia, parte, test, totalSum);

        }       
        
    }
}


using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;

namespace AdventOfCodeCSharp.Y2023
{

    public partial class Program
    {
        public static void Dia01_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, true);

            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            int totalSum = 0;

            foreach (var line in lines)
            {
                int firstDigit = 0;
                int lastDigit = 0;
                // Encontrar el primer dígito
                foreach (char c in line)
                {
                    if (char.IsDigit(c))
                    {
                        firstDigit = c - '0';
                        break;
                    }
                }

                // Encontrar el último dígito
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(line[i]))
                    {
                        lastDigit = line[i] - '0';
                        break;
                    }
                }

                if (firstDigit != -1 && lastDigit != -1)
                {
                    int calibrationValue = firstDigit * 10 + lastDigit;
                    totalSum += calibrationValue;
                }
            }
            var resultados = AdventOfCodeCSharp.Program.GetResults(year);
            var ok = false;
            var resultado = -1;
            if (test)
            {
                resultado = resultados[dia].Test[parte - 1];
                ok = totalSum == resultado;
            }
            else
            {
                resultado = resultados[dia].Input[parte - 1];
                ok = totalSum == resultado;
            }
            if (ok)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"CORRECTO: {totalSum}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"INCORRECTO: La suma te da {totalSum} y el resultado es {resultado} ");
            }
            
        }
        
    }
}

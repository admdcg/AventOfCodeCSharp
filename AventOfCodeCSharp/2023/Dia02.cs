using System;
using System.Collections.Generic;

namespace AdventOfCodeCSharp.Y2023
{
    public partial class Program
    {
        public static void Dia02_1()
        {
            string filePath = "2023\\inputs\\dia01-1-test.txt";
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            int Suma = 0;

            foreach (var line in lines)
            {
                int firstDigit = -1;
                int lastDigit = -1;

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
                    Suma += calibrationValue;
                }
            }

            Console.WriteLine($"La suma total de los valores de calibración es: {Suma}");
        }
    }


}

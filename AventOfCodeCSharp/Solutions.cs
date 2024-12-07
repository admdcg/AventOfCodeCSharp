using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Solutions
    {
        public static void Summary(int year, int dia, int parte, bool test, long totalSum)
        {
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

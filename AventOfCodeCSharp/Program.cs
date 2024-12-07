using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using AventOfCodeCSharp;

namespace AdventOfCodeCSharp
{
    public partial class Program
    {
        public static void Main()
        {            
            //Y2023.Program.Dia01_1(2023,1,1,true);
            //Y2024.Program.Dia03_2(2024, 3, 2, false, false);
            Y2024.Program.Dia08(year: 2024, dia: 8, parte: 1, test: true, other2Test: false);
        }
        public static Dictionary<long, Resultado> GetResults(int year)
        {
            string appDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(appDirectory, year.ToString(), "inputs", "Resultados.txt"); // Ruta del archivo
          
            List<string> lines = new List<string>(); // Lista para almacenar las líneas            
            lines = new List<string>(File.ReadAllLines(filePath));
            var resultados = new Dictionary<long, Resultado>();
            foreach (string line in lines)
            {
                //Console.WriteLine(line);
                var listSplited = line.Split(':');
                var dia = long.Parse(listSplited[0].Split(' ')[1]);
                var results = listSplited[1].SplitNumbers<long>();
                var resultado = new Resultado()
                {
                    Test = [results[0], results[2]],
                    Input = [results[1], results[3]]
                };
                resultados.Add(dia, resultado);
            }
            return resultados;
        }
        public static string GetFilePath(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            var parteStr = other2Test ? $"-{parte}" : "";
            var testStr = test ? "-test": "";
            string appDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(appDirectory, year.ToString(), "inputs", $"Dia{dia.ToString("D2")}{parteStr}{testStr}.txt"); 
            return filePath;
        }
    }
}

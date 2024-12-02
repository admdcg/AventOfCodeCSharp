using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeCSharp.Y2024
{
    public partial class Program
    {
        public static void Dia01_1(string[] args)
        {
            string filePath = "2024\\inputs\\Dia01.txt"; // Ruta del archivo
            List<string> lines = new List<string>(); // Lista para almacenar las líneas
            var lista1 = new List<int>();
            var lista2 = new List<int>();
            try
            {
                // Leer todas las líneas del archivo y agregarlas a la lista
                lines = new List<string>(File.ReadAllLines(filePath));
                int suma = 0;
                // Mostrar las líneas para verificar
                //Console.WriteLine("Líneas del archivo:");
                foreach (string line in lines)
                {
                    //Console.WriteLine(line);
                    var twoNum = line.Split(' ');

                    lista1.Add(int.Parse(twoNum[0]));
                    lista2.Add(int.Parse(twoNum[twoNum.Count() - 1]));

                }
                var listaO1 = lista1.OrderBy(i => i).ToList();
                var listaO2 = lista2.OrderBy(i => i).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    var num1 = listaO1[i];

                    var num2 = listaO2[i];
                    Console.WriteLine($"{num1} {num2}");
                    //Console.WriteLine($"Num2: {num2}");
                    suma = suma + Math.Abs(num2 - num1);
                }
                Console.WriteLine("Suma: " + suma.ToString());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"El archivo '{filePath}' no fue encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
    }

}
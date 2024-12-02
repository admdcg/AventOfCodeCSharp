using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCodeCSharp.Y2024
{
    public partial class Program
    {
        public static void Dia01_2(string[] args)
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
                var dic2 = new Dictionary<int, int>();
                var listaO1 = lista1.OrderBy(i => i).ToList();
                var listaO2 = lista2.OrderBy(i => i).ToList();
                foreach (var item in listaO2)
                {
                    if (dic2.ContainsKey(item))
                    {
                        dic2[item] = dic2[item] + 1;
                    }
                    else
                    {
                        dic2.Add(item, 1);
                    }                
                }
                foreach (var item in lista1)            
                {       
                    int repetidos = 0;
                    if (dic2.ContainsKey(item))
                    {
                        repetidos = dic2[item];
                    }
                    Console.WriteLine($"{item} {repetidos}");
                    suma = suma + (item * repetidos);
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

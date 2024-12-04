using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCodeCSharp.Y2024
{
    public partial class Program
    {
        public static void Dia02_1(string[] args)
        {
            string filePath = "2024\\inputs\\Dia02.txt"; // Ruta del archivo
            List<string> lines = new List<string>(); // Lista para almacenar las líneas
            var lista1 = new List<int>();
            var lista2 = new List<int>();
            try
            {
                // Leer todas las líneas del archivo y agregarlas a la lista
                lines = new List<string>(File.ReadAllLines(filePath));
                int suma = 0;
                var lista = new List<int>();
                foreach (string line in lines)
                {
                    //Console.WriteLine(line);
                    var numbersStr = line.Split(' ');
                    lista = numbersStr.Select(int.Parse).ToList();
                    var anterior = lista[0];
                    bool vaCreciendo = lista[1] > lista[0] ? true : false;
                    bool seguro = true;
                    for (int i = 1; i < lista.Count(); i++)
                    {
                        var distancia = Math.Abs(lista[i] - lista[i - 1]);
                        if (distancia < 1 || distancia > 3)
                        {
                            seguro= false;
                            break;
                        }
                        if ((lista[i] > anterior && !vaCreciendo) || lista[i] < anterior && vaCreciendo)
                        {
                            seguro = false;
                            break;
                        }                    
                        anterior = lista[i];
                    }
                    if (seguro)
                    {
                        suma = suma + 1;
                    }
                }            
                Console.WriteLine("Seguros: " + suma.ToString());
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
        public static void Dia02_2()
        {
            string appDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(appDirectory, "2024", "inputs", "Dia02.txt"); // Ruta del archivo
            try
            {
                // Leer todas las líneas del archivo y agregarlas a la lista
                List<string> lines = new List<string>(); // Lista para almacenar las líneas            
                lines = new List<string>(File.ReadAllLines(filePath));
                int suma = 0;
                foreach (string line in lines)
                {
                    //Console.WriteLine(line);                
                    var numbersStr = line.Split(' ');
                    var lista = line.SplitNumbers();
                    if (EsSeguro(lista))
                    {
                        suma += 1;
                    }
                    else
                    {
                        for (int i = 0; i < lista.Count(); i++)
                        {
                            var newLista = new List<int>(lista);
                            newLista.RemoveAt(i);
                            if (EsSeguro(newLista))
                            {
                                suma += 1;
                                break;
                            }
                        }
                    }
                }
                Console.WriteLine("Seguros: " + suma.ToString());
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
        private static Boolean EsSeguro(List<int> lista)
        {
            var anterior = lista[0];
            bool vaCreciendo = lista[1] > anterior ? true : false;
            bool seguro = true;
            for (int i = 1; i < lista.Count(); i++)
            {
                var distancia = Math.Abs(lista[i] - anterior);
                if (distancia < 1 || distancia > 3)
                {
                    seguro = false;
                    break;
                }
                if ((lista[i] > anterior && !vaCreciendo) || lista[i] < anterior && vaCreciendo)
                {
                    seguro = false;
                    break;
                }
                anterior = lista[i];
            }
            return seguro;
        }    }
}

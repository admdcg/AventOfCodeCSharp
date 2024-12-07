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
        public static void Dia07(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia07_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia07_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia07_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var DictAntes = new Dictionary<int, List<int>>();
            var DictDespues = new Dictionary<int, List<int>>();
            long totalSum = 0;

            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                var numeros = StringHelper.SplitNumbers(lines[f]);
                var solucion = numeros[0];
                numeros.RemoveAt(0);                
                var permutaciones = GetPermutaciones(numeros.Count() - 1);
                foreach (var permutacion in permutaciones)
                {
                    var resultado = CalculaEcuacion(numeros, permutacion);
                    var ecuacionStr = GetEcuacion(numeros, permutacion);
                    if (resultado == solucion)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{ecuacionStr} = {resultado}. Solucion: {solucion}");
                        totalSum += resultado;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{ecuacionStr} = {resultado}. Solucion: {solucion}");
                    }
                    
                }
            }
            //18903718232 NO Correcto
            Summary(year, dia, parte, test, totalSum);
        }
        public static List<List<char>> GetPermutaciones(int n)
        {
            char[] chars = { '+', '*' };
            int total = (int)Math.Pow(2, n);
            var permutaciones = new List<List<char>>();

            for (int i = 0; i < total; i++)
            {
                // Generar la representación binaria con longitud n
                string binary = Convert.ToString(i, 2).PadLeft(n, '0');
                // Reemplazar 0 por + y 1 por *
                var permutacion = new List<char>();
                foreach (char bit in binary)
                {
                    permutacion.Add((bit == '0') ? '+' : '*');
                }
                permutaciones.Add(permutacion);
            }
            return permutaciones;

        }
        public static string GetEcuacion(List<int> numeros, List<char> operadores)
        {
            var ecuacion = "";
            for (int i = 0; i < numeros.Count(); i++)
            {
                ecuacion += numeros[i];
                if (i < operadores.Count())
                {
                    ecuacion += operadores[i];
                }
            }
            return ecuacion;
        }       
        public static int CalculaEcuacion(List<int> numeros, List<char> operadores)
        {
            var resultado = numeros[0];
            for (int i = 1; i < numeros.Count() ; i++)
            {
                if (operadores[i-1] == '+')
                {
                    resultado += numeros[i];
                }
                else if (operadores[i-1] == '*')
                {
                    resultado *= numeros[i];
                }
            }
            return resultado;            
        }

        public static void Dia07_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var DictAntes = new Dictionary<int, List<int>>();
            var DictDespues = new Dictionary<int, List<int>>();
            int totalSum = 0;

            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                if (line.Contains('|'))
                {
                    var split = line.Split('|');
                    var antes = int.Parse(split[0]);
                    var despues = int.Parse(split[1]);
                    if (!DictAntes.ContainsKey(antes))
                    {
                        DictAntes.Add(antes, new List<int>() { despues });
                    }
                    else
                    {
                        DictAntes[antes].Add(despues);
                    }
                    if (!DictDespues.ContainsKey(despues))
                    {
                        DictDespues.Add(despues, new List<int>() { antes });
                    }
                    else
                    {
                        DictDespues[despues].Add(antes);
                    }
                }
                else if (line.Contains(','))
                {
                    var split = line.Split(',');
                    var actualizaciones = new List<int>(split.Select(x => int.Parse(x)));
                    Boolean esCorrecta = true;
                    Helper.PrintList(actualizaciones);
                    var estadoActualizacion = EsActualizacionCorrecta(DictAntes, DictDespues, actualizaciones);
                    if (!estadoActualizacion.ReglaIncumplida)
                    {
                        continue;
                    }
                    while (estadoActualizacion.ReglaIncumplida)
                    {
                        actualizaciones = GetNewActualizaciones(actualizaciones, estadoActualizacion.Actualizacion, estadoActualizacion.Numero);
                        Helper.PrintList(actualizaciones);
                        estadoActualizacion = EsActualizacionCorrecta(DictAntes, DictDespues, actualizaciones);
                    }
                    var intermedio = actualizaciones.Count() / 2;
                    int numeroIntermedio = actualizaciones[intermedio];
                    Console.WriteLine(string.Join(", ", actualizaciones));
                    Console.WriteLine($"Lista correcta intermedio: {intermedio} -> {numeroIntermedio}");
                    totalSum += numeroIntermedio;
                }
            }
            Summary(year, dia, parte, test, totalSum);
        }

        private static EstadoActualizacion EsActualizacionCorrecta(Dictionary<int, List<int>> DictAntes, Dictionary<int, List<int>> DictDespues, List<int> actualizaciones)
        {
            var retEstadoActualizacion = new EstadoActualizacion()
            {
                ReglaIncumplida = false,
            };

            for (int i = 0; i < actualizaciones.Count(); i++)
            {
                var actualizacion = actualizaciones[i];
                if (!DictAntes.ContainsKey(actualizacion))
                {
                    DictAntes.Add(actualizacion, new List<int>());
                }
                if (!DictDespues.ContainsKey(actualizacion))
                {
                    DictDespues.Add(actualizacion, new List<int>());
                }
                if (i > 0)
                {
                    var anteriores = actualizaciones.GetRange(0, i);
                    if (anteriores.Any(a => DictAntes[actualizacion].Contains(a)))
                    {
                        foreach (var p in anteriores)
                        {
                            foreach (var d in DictAntes[actualizacion])
                            {
                                if (p == d)
                                {
                                    retEstadoActualizacion.ReglaIncumplida = true;
                                    retEstadoActualizacion.Diccionario = 0;
                                    retEstadoActualizacion.Actualizacion = actualizacion;
                                    retEstadoActualizacion.Numero = p;
                                    return retEstadoActualizacion;
                                }
                            }
                        }
                    }
                }
                if (i < actualizaciones.Count() - 2)
                {
                    var posteriores = actualizaciones.GetRange(i + 1, actualizaciones.Count() - 1 - i);
                    foreach (var p in posteriores)
                    {
                        foreach (var d in DictDespues[actualizacion])
                        {
                            if (p == d)
                            {
                                retEstadoActualizacion.ReglaIncumplida = true;
                                retEstadoActualizacion.Diccionario = 1;
                                retEstadoActualizacion.Actualizacion = actualizacion;
                                retEstadoActualizacion.Numero = p;
                                return retEstadoActualizacion;
                            }
                        }
                    }
                }
            }
            return retEstadoActualizacion;
        }
        private class EstadoActualizacion
        {
            public bool ReglaIncumplida { get; set; }
            public int Diccionario { get; set; } //0-antes, 1-despues
            public int Actualizacion { get; set; }
            public int Numero { get; set; }
        }

        private static List<int> GetNewActualizaciones(List<int> actualizaciones, int actualizacion, int p)
        {
            var newActualizaciones = new List<int>();
            for (int j = 0; j < actualizaciones.Count(); j++)
            {
                if (actualizaciones[j] == actualizacion)
                {
                    newActualizaciones.Add(p);
                }
                else if (actualizaciones[j] == p)
                {
                    newActualizaciones.Add(actualizacion);
                }
                else
                {
                    newActualizaciones.Add(actualizaciones[j]);
                }
            }
            return newActualizaciones;
        }

        public static List<List<int>> ObtenerPermutaciones(List<int> lista)
        {
            var resultado = new List<List<int>>();
            GenerarPermutaciones(lista, 0, resultado);
            return resultado;
        }

        private static void GenerarPermutaciones(List<int> lista, int indice, List<List<int>> resultado)
        {
            if (indice == lista.Count)
            {
                resultado.Add(new List<int>(lista));
            }
            else
            {
                for (int i = indice; i < lista.Count; i++)
                {
                    Intercambiar(lista, indice, i);
                    GenerarPermutaciones(lista, indice + 1, resultado);
                    Intercambiar(lista, indice, i); // Deshacer el intercambio
                }
            }
        }

        private static void Intercambiar(List<int> lista, int i, int j)
        {
            int temp = lista[i];
            lista[i] = lista[j];
            lista[j] = temp;
        }
    }
}



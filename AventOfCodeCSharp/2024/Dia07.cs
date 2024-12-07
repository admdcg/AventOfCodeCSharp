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
                var numeros = StringHelper.SplitNumbers<long>(lines[f]);
                var solucion = numeros[0];
                numeros.RemoveAt(0);                
                var permutaciones = GetPermutaciones(numeros.Count() - 1);
                foreach (var permutacion in permutaciones)
                {
                    var resultado = CalculaEcuacion(numeros, permutacion);
                    var ecuacionStr = GetEcuacionString(numeros, permutacion);
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
        public static void Dia07_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var DictAntes = new Dictionary<int, List<int>>();
            var DictDespues = new Dictionary<int, List<int>>();
            long totalSum = 0;

            for (int f = 0; f < lines.Count(); f++)
            {
                var line = lines[f];
                var numeros = StringHelper.SplitNumbers<long>(lines[f]);
                var solucion = numeros[0];
                long resultado = 0;
                numeros.RemoveAt(0);
                var permutaciones = GetPermutaciones(numeros.Count() - 1);
                foreach (var permutacion in permutaciones)
                {
                    resultado = CalculaEcuacion(numeros, permutacion);
                    var ecuacionStr = GetEcuacionString(numeros, permutacion);
                    if (resultado == solucion)
                    {
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine($"{ecuacionStr} = {resultado}. Solucion: {solucion}");
                        totalSum += resultado;
                        break;
                    }
                    else
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine($"{ecuacionStr} = {resultado}. Solucion: {solucion}");
                    }
                }
                if (resultado != solucion)
                {
                    //var concatenados = GetAllConcatenated(numeros);                    
                    Console.ForegroundColor = ConsoleColor.White;                        
                    var permutacionesConcat = GetPermutacionesConcat(numeros.Count() - 1);
                    foreach (var permutacion in permutacionesConcat)
                    {
                        resultado = CalculaEcuacion(numeros, permutacion);
                        var ecuacionStr = GetEcuacionString(numeros, permutacion);
                        if (resultado == solucion)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Línea: {f} => {ecuacionStr} = {resultado}. Solucion: {solucion}");
                            totalSum += resultado;
                            break;
                        }
                        else
                        {
                            //Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine($"{ecuacionStr} = {resultado}. Solucion: {solucion}");
                        }
                    }                    
                }
            }
            //18903718232 NO Correcto
            Summary(year, dia, parte, test, totalSum);
        }
        public static List<List<long>> GetAllConcatenated(List<long> numeros)
        {
            var result = new List<List<long>>();
            for (int i = 0; i < numeros.Count() -1; i++)
            {
                var list = new List<long>();
                for (int j = 0; j < numeros.Count(); j++)
                {
                    if (i==j)
                    {
                        var concatenado = long.Parse(numeros[j].ToString() + numeros[j+1].ToString());
                        list.Add(concatenado);
                        j++;
                    }
                    else
                    {
                        list.Add(numeros[j]);
                    }                    
                }
                result.Add(list);
            }
            return result;
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
        public static string ConvertToBase3(int number)
        {
            if (number == 0) return "0";

            string result = "";
            int base3 = 3;

            while (number > 0)
            {
                int remainder = number % base3;
                result = remainder.ToString() + result;
                number /= base3;
            }

            return result;
        }
        public static List<List<string>> GetPermutacionesConcat(int n)
        {            
            int total = (int)Math.Pow(3, n);
            var permutaciones = new List<List<string>>();

            for (int i = 0; i < total; i++)
            {
                // Generar la representación en base 3 con longitud n
                string binary = ConvertToBase3(i).PadLeft(n, '0');
                // Reemplazar 0 por + y 1 por * y 2 por ||
                var permutacion = new List<string>();
                foreach (char num in binary)
                {
                    if (num == '0')
                    {
                        permutacion.Add("+");
                    }
                    else if (num == '1')
                    {
                        permutacion.Add("*");
                    }
                    else
                    {
                        permutacion.Add("||");
                    }
                }
                permutaciones.Add(permutacion);
            }
            return permutaciones;

        }
        public static string GetEcuacionString(List<long> numeros, List<char> operadores)
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
        public static string GetEcuacionString(List<long> numeros, List<string> operadores)
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
        public static long CalculaEcuacion(List<long> numeros, List<char> operadores)
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
        public static long CalculaEcuacion(List<long> numeros, List<String> operadores)
        {
            var resultado = numeros[0];
            for (int i = 1; i < numeros.Count(); i++)
            {
                if (operadores[i - 1] == "+")
                {
                    resultado += numeros[i];
                }
                else if (operadores[i - 1] == "*")
                {
                    resultado *= numeros[i];
                }
                else if (operadores[i - 1] == "||")
                {
                    resultado = long.Parse(resultado.ToString() + numeros[i].ToString());
                }
            }
            return resultado;
        }
    }
}



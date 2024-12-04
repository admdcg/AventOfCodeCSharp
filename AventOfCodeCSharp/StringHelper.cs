﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCodeCSharp
{
    public static class StringHelper
    {
        public static List<T> SplitRegex<T>(this string cadena, string patronRegex)
        {
            var regex = new Regex(patronRegex);
            var lista = new List<T>();
            foreach (Match m in regex.Matches(cadena))
            {
                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(m.Value, out int intValue))
                    {
                        lista.Add((T)(object)intValue);
                    }
                }
                else if (typeof(T) == typeof(string))
                {
                    lista.Add((T)(object)m.Value);
                }
            }
            return lista;
        }
        public static List<int> SplitNumbers(this string cadena)
        {
            var lista = cadena.SplitRegex<int>("\\d+");
            return lista;
        }
        public static List<string> SplitWords(this string cadena)
        {
            var lista = cadena.SplitRegex<string>("\\w+");            
            return lista;
        }
        public static List<string> SplitTokens(this string cadena)
        {
            var lista = cadena.SplitRegex<string>("\\S+");
            return lista;
        }        
    }
}
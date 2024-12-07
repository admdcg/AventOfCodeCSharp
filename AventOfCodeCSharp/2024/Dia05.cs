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
        //public static void Dia05(int year, int dia, int parte, bool test, bool other2Test = false)
        //{
        //    if (parte == 1)
        //    {
        //        Dia05_1(year, dia, parte, test, other2Test);
        //    }
        //    else
        //    {
        //        Dia05_2(year, dia, parte, test, other2Test);
        //    }
        //}
        //public static void Dia05_1(int year, int dia, int parte, bool test, bool other2Test = false)
        //{
        //    string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
        //    List<string> lines = new List<string>(File.ReadAllLines(filePath));
        //    var DictAntes = new Dictionary<int, List<int>>();
        //    var DictDespues = new Dictionary<int, List<int>>();
        //    int totalSum = 0;

        //    for (int f = 0; f < lines.Count(); f++)
        //    {
        //        var line = lines[f];
        //        if (line.Contains('|'))
        //        {
        //            var split = line.Split('|');
        //            var antes = int.Parse(split[0]);
        //            var despues = int.Parse(split[1]);
        //            if (!DictAntes.ContainsKey(antes))
        //            {
        //                DictAntes.Add(antes, new List<int>() { despues });
        //            }
        //            else
        //            {
        //                DictAntes[antes].Add(despues);
        //            }
        //            if (!DictDespues.ContainsKey(despues))
        //            {
        //                DictDespues.Add(despues, new List<int>() { antes });
        //            }
        //            else
        //            {
        //                DictDespues[despues].Add(antes);
        //            }

        //        }
        //        else if (line.Contains(','))
        //        {
        //            var split = line.Split(',');
        //            var actualizaciones = new List<int>(split.Select(x => int.Parse(x)));
        //            Boolean esCorrecta = true;
        //            Helper.PrintList(actualizaciones);
        //            for (int i = 0; i < actualizaciones.Count(); i++)
        //            {
        //                var actualizacion = actualizaciones[i];
        //                if (!DictAntes.ContainsKey(actualizacion))
        //                {
        //                    DictAntes.Add(actualizacion, new List<int>());
        //                }
        //                if (!DictDespues.ContainsKey(actualizacion))
        //                {
        //                    DictDespues.Add(actualizacion, new List<int>());
        //                }
        //                if (i > 0)
        //                {
        //                    var anteriores = actualizaciones.GetRange(0, i);
        //                    if (anteriores.Any(a => DictAntes[actualizacion].Contains(a)))
        //                    {
        //                        esCorrecta = false;
        //                        break;
        //                    }
        //                }
        //                if (i < actualizaciones.Count() - 2)
        //                {
        //                    var posteriores = actualizaciones.GetRange(i + 1, actualizaciones.Count() - 1 - i);
        //                    if (posteriores.Any(p => DictDespues[actualizacion].Contains(p)))
        //                    {
        //                        esCorrecta = false;
        //                        break;
        //                    }
        //                }
        //            }
        //            if (esCorrecta)
        //            {
        //                var intermedio = actualizaciones.Count() / 2;
        //                Console.WriteLine($"Lista correcta intermedio: {intermedio} -> {actualizaciones[intermedio]}");
        //                totalSum += actualizaciones[intermedio];
        //            }
        //        }

        //    }
        //    Summary(year, dia, parte, test, totalSum);
        //}

        //public static void Dia05_2(int year, int dia, int parte, bool test, bool other2Test = false)
        //{
        //    string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
        //    List<string> lines = new List<string>(File.ReadAllLines(filePath));
        //    var DictAntes = new Dictionary<int, List<int>>();
        //    var DictDespues = new Dictionary<int, List<int>>();
        //    int totalSum = 0;

        //    for (int f = 0; f < lines.Count(); f++)
        //    {
        //        var line = lines[f];
        //        if (line.Contains('|'))
        //        {
        //            var split = line.Split('|');
        //            var antes = int.Parse(split[0]);
        //            var despues = int.Parse(split[1]);
        //            if (!DictAntes.ContainsKey(antes))
        //            {
        //                DictAntes.Add(antes, new List<int>() { despues });
        //            }
        //            else
        //            {
        //                DictAntes[antes].Add(despues);
        //            }
        //            if (!DictDespues.ContainsKey(despues))
        //            {
        //                DictDespues.Add(despues, new List<int>() { antes });
        //            }
        //            else
        //            {
        //                DictDespues[despues].Add(antes);
        //            }
        //        }
        //        else if (line.Contains(','))
        //        {
        //            var split = line.Split(',');
        //            var actualizaciones = new List<int>(split.Select(x => int.Parse(x)));
        //            Boolean esCorrecta = true;
        //            Helper.PrintList(actualizaciones);
        //            var estadoActualizacion = EsActualizacionCorrecta(DictAntes, DictDespues, actualizaciones);
        //            if (!estadoActualizacion.ReglaIncumplida)
        //            {
        //                continue;
        //            }
        //            while (estadoActualizacion.ReglaIncumplida)
        //            {
        //                actualizaciones = GetNewActualizaciones(actualizaciones, estadoActualizacion.Actualizacion, estadoActualizacion.Numero);
        //                Helper.PrintList(actualizaciones);
        //                estadoActualizacion = EsActualizacionCorrecta(DictAntes, DictDespues, actualizaciones);
        //            }
        //            var intermedio = actualizaciones.Count() / 2;
        //            int numeroIntermedio = actualizaciones[intermedio];
        //            Console.WriteLine(string.Join(", ", actualizaciones));
        //            Console.WriteLine($"Lista correcta intermedio: {intermedio} -> {numeroIntermedio}");
        //            totalSum += numeroIntermedio;
        //        }
        //    }
        //    Summary(year, dia, parte, test, totalSum);
        //}
    }
}


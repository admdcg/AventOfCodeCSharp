using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using AdventOfCodeCSharp;
using AventOfCodeCSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Utilities.Geometry.Euclidean;
using JetBrains.Annotations;
using System.Data.Common;
using System.Runtime.InteropServices.Marshalling;


namespace AdventOfCodeCSharp.Y2024
{
    public partial class Program : Solutions
    {
        public static void Dia06(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            if (parte == 1)
            {
                Dia06_1(year, dia, parte, test, other2Test);
            }
            else
            {
                Dia06_2(year, dia, parte, test, other2Test);
            }
        }
        public static void Dia06_1(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
            //filePath = Path.Combine(AppContext.BaseDirectory, year.ToString(), "inputs", $"dia10-A.txt");
            int totalSum = 0;
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            var laberinto = new Laberinto(lines, new Dictionary<char, TurnsType> { { Laberinto.UP, TurnsType.Right} });
            int steps = laberinto.Walk('X');            
            laberinto.Print();
            totalSum = laberinto.Lines.Sum(l => l.Count(c => c == 'X'));
            Summary(year, dia, parte, test, totalSum);
        }
        public static void Dia06_2(int year, int dia, int parte, bool test, bool other2Test = false)
        {
            try
            {
                string filePath = AdventOfCodeCSharp.Program.GetFilePath(year, dia, parte, test, other2Test);
                //filePath = Path.Combine(AppContext.BaseDirectory, year.ToString(), "inputs", $"dia10-A.txt");
                int totalSum = 0;
                List<string> lines = new List<string>(File.ReadAllLines(filePath));
                var laberinto = new Laberinto(lines, new Dictionary<char, TurnsType> { { Laberinto.UP, TurnsType.Right } });
                int steps = laberinto.Walk('X');
                laberinto.Print();
                totalSum = laberinto.Lines.Sum(l => l.Count(c => c == 'X'));
                Summary(year, dia, parte, test, totalSum);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: "+ex.Message);                
            }
            
        }

        private static double NormalizeAngle(double angle)
        {
            angle -= (Math.PI / 2);
            if (angle < -Math.PI)
            {
                angle += Math.PI * 2;
            }
            return angle;
        }

        private static List<Asteroid> GetAsteroids(MapText mapText)
        {
            var asteroids = new List<Asteroid>();
            for (int row = 0; row < mapText.Height; row++)
            {
                for (int col = 0; col < mapText.Length; col++)
                {
                    if (mapText.Lines[row][col] == '#')
                    {
                        var asteorid = new Asteroid(mapText.GetPoint(row, col));
                        asteroids.Add(asteorid);
                    }
                }
            }
            return asteroids;
        }

        private static (Asteroid?, int) FindBestLocation(List<Asteroid> asteroids)
        {
            int maxVisible = 0;
            Asteroid? bestLocation = null;

            foreach (var asteroid in asteroids)
            {
                var visibleAsteroids = CountVisibleAsteroids(asteroid, asteroids);
                if (visibleAsteroids > maxVisible)
                {
                    maxVisible = visibleAsteroids;
                    bestLocation = asteroid;
                }
            }

            return (bestLocation, maxVisible);
        }

        private static int CountVisibleAsteroids(Asteroid origin, List<Asteroid> asteroids)
        {
            var angles = new HashSet<double>();

            foreach (var asteroid in asteroids)
            {
                if (!origin.IsEqual(asteroid))
                {
                    double angle = Math.Atan2(asteroid.Point.Row - origin.Point.Row, asteroid.Point.Column - origin.Point.Column);
                    angles.Add(angle);
                }
            }

            return angles.Count;
        }
        private static Dictionary<double, List<Asteroid>> GetAngleWithAsteroids(Asteroid origin, List<Asteroid> asteroids)
        {
            var angles = new Dictionary<double, List<Asteroid>>();

            foreach (var asteroid in asteroids)
            {
                if (!origin.IsEqual(asteroid))
                {
                    double angle = Math.Atan2(asteroid.Point.Row - origin.Point.Row, asteroid.Point.Column - origin.Point.Column);
                    asteroid.Distance = Point.Distance(origin.Point, asteroid.Point);
                    if (angles.ContainsKey(angle))
                    {
                        angles[angle].Add(asteroid);
                    }
                    else
                    {
                        angles.Add(angle, new List<Asteroid> { asteroid });
                    }
                }
            }
            foreach (var angle in angles) //Se ordenan por distancia, los más cercanos primero
            {
                angles[angle.Key] = angle.Value.OrderBy(a => a.Distance).ToList();
            }
            return angles;
        }

        private static List<Line> GetReverseLines(MapText mapText, List<Line> rectas)
        {
            var rectasInversas = new List<Line>();
            foreach (var recta in rectas)
            {
                var rectaInv = mapText.ReverseLine(recta);
                rectasInversas.Add(rectaInv);
            }
            return rectasInversas;
        }

        public static void Print2Puntos(Point point1, Point point2)
        {
            Console.WriteLine($"({point1.Row},{point1.Column})({point2.Row},{point2.Column})");
        }
        public static void PrintPunto(Point point)
        {
            Console.WriteLine($"({point.Row},{point.Column})");
        }
        public static void PrintRectas(String header, List<Line> rectas)
        {
            Console.WriteLine(header);
            foreach (var recta in rectas)
            {
                Console.WriteLine(recta.Value);
            }
        }
        public static void PrintList<T>(IEnumerable<T> lst)
        {

            foreach (var c in lst)
            {
                Console.WriteLine((T)c);
            }
        }
        private class Asteroid
        {
            public Asteroid(Point point) => Point = point;
            public Point Point { get; set; }
            public double Distance { get; set; }
            public bool IsEqual(Asteroid other)
            {
                return Point.IsEqual(other.Point);
            }
            public override string ToString()
            {
                return $"({Point.Row},{Point.Column}): {Point.Value}";
            }
        }

    }
}




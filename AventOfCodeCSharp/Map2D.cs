using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class Map2D
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public char[,] Map { get; private set; }
        public Map2D(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new char[width, height];
        }
        public void Set(int x, int y, char value)
        {
            Map[x, y] = value;
        }
        public char Get(int x, int y)
        {
            return Map[x, y];
        }
        public void Print()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Map[x, y]);
                }
                Console.WriteLine();
            }
        }
        public void Print(int x, int y)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == y && j == x)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(Map[j, i]);
                    }
                }
                Console.WriteLine();
            }
        }        
    }
}

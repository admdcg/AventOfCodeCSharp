using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public enum TurnsType
    {
        Up,
        Down,
        Right,
        Left
    }
    public class Laberinto : MapText
    {
        public static readonly char WALL = '#';
        public static readonly char UP = '^';
        public static readonly char DOWN = 'v';
        public static readonly char RIGHT = '>';
        public static readonly char LEFT = '<';
        private static readonly Dictionary<char, TurnsType> MapTurns = new Dictionary<char, TurnsType>
        {
            { UP,  TurnsType.Up },
            { DOWN, TurnsType.Down },
            { RIGHT, TurnsType.Right },
            { LEFT, TurnsType.Left }
        };
        public Dictionary<char, TurnsType> Turns { get; set; }
        public List<Point> Walls { get; set; }
        public Point InitPoint { get; set; }
        public TurnsType Direction { get; set; }
        public Laberinto(List<string> lines, Dictionary<char, TurnsType> turns) : base(lines)
        {
            Walls = GetWalls();
        }
        private List<Point> GetWalls()
        {
            var walls = new List<Point>();
            for (int row = 0; row < base.Height; row++)
            {
                for (int column = 0; column < base.Length; column++)
                {
                    char value = Map[row, column];
                    if (Map[row, column] == MapText.EMPTY)
                    {
                        continue;
                    }
                    else if (Map[row, column] == WALL)
                    {
                        walls.Add(new Point(row, column, ref value));
                    }
                    else if (value == UP || value == DOWN || value == RIGHT || value == LEFT)
                    {
                        base.Position = new Point(row, column, ref value);
                        Direction = MapTurns[value];
                        base.SetCharAt(row, column, MapText.EMPTY);
                    }
                }
            }           
            return walls;
        }
        public void SetCrumb(char? crumb)
        {
            if (crumb != null)
            {                
                base.SetCharAt(Position.Row, Position.Column, (char)crumb);
            }
            
        }
        public int Walk(char? crumb = null)
        {
            int steps = 0;
            SetCrumb(crumb);
            for (;;)
            {
                if (Direction == TurnsType.Up)
                {
                    if (base.Up())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Down();
                            Direction = TurnsType.Right;
                        }
                        SetCrumb(crumb);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == TurnsType.Right)
                {
                    if (base.Right())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Left();
                            Direction = TurnsType.Down;
                        }
                        SetCrumb(crumb);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == TurnsType.Down)
                {
                    if (base.Down())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Up();
                            Direction = TurnsType.Left;
                        }
                        SetCrumb(crumb);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == TurnsType.Left)
                {
                    if (base.Left())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Right();
                            Direction = TurnsType.Up;
                        }
                        SetCrumb(crumb);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return steps;
        }


    }
}

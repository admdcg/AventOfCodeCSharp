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
    public enum DirectionType
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
        private static readonly Dictionary<char, DirectionType> MapDirections = new Dictionary<char, DirectionType>
        {
            { UP,  DirectionType.Up },
            { DOWN, DirectionType.Down },
            { RIGHT, DirectionType.Right },
            { LEFT, DirectionType.Left }
        };
        public Dictionary<char, DirectionType> Turns { get; set; }
        public List<Point> Walls { get; set; }
        public Point InitPoint { get; set; }
        public DirectionType InitDirection { get; set; }
        public DirectionType Direction { get; set; }

        public List<Scrumb> Scrumbs { get; set; }
        public Laberinto(List<string> lines, Dictionary<char, DirectionType> turns) : base(lines)
        {
            Walls = GetWalls();
            Scrumbs = new List<Scrumb>();
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
                        walls.Add(new Point(row, column, value));
                    }
                    else if (value == UP || value == DOWN || value == RIGHT || value == LEFT)
                    {
                        InitPoint = GetPoint(row, column);
                        base.Position = new Point(row, column, value);
                        Direction = MapDirections[value];
                        InitDirection = Direction;
                        base.SetCharAt(row, column, MapText.EMPTY);
                    }
                }
            }
            return walls;
        }
        public Scrumb SetCrumb(char crumb)
        {
            base.SetCharAt(Position.Row, Position.Column, crumb);
            var point = new Point(Position.Row, Position.Column);
            var scrum = new Scrumb(point.Row, point.Column, crumb, Direction);
            Scrumbs.Add(scrum);
            return scrum;
        }
        public (int, bool) Walk(char crumbChar)
        {
            int steps = 0;
            Scrumb scrumb = SetCrumb(crumbChar);
            bool inLoop = false;
            for (; ; )
            {
                if (Direction == DirectionType.Up)
                {
                    if (base.Up())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Down();
                            Direction = DirectionType.Right;
                        }
                        scrumb = SetCrumb(crumbChar);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == DirectionType.Right)
                {
                    if (base.Right())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Left();
                            Direction = DirectionType.Down;
                        }
                        scrumb = SetCrumb(crumbChar);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == DirectionType.Down)
                {
                    if (base.Down())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Up();
                            Direction = DirectionType.Left;
                        }
                        scrumb = SetCrumb(crumbChar);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (Direction == DirectionType.Left)
                {
                    if (base.Left())
                    {
                        if (Position.Value == WALL)
                        {
                            base.Right();
                            Direction = DirectionType.Up;
                        }
                        scrumb = SetCrumb(crumbChar);
                        steps++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (Scrumbs.Where(s => s.IsEqual(scrumb)).Count() >= 2)
                {
                    inLoop = true;
                    break;
                }
            }
            return (steps, inLoop);
        }
        public void RemoveScrumbs()
        {
            foreach (var scrumb in Scrumbs)
            {
                base.SetCharAt(scrumb.Row, scrumb.Column, MapText.EMPTY);
            }
            Scrumbs.Clear();
        }
    }
}


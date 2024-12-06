using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class MapText
    {
        public static readonly char EMPTY = '.';
        public List<string> Lines { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public List<Point> Points { get; set; }
        public Point Position { get; set; }        
        public MapText(List<string> parlines)
        {
            if (!parlines.Any())
            {
                throw new Exception("No hay líneas en el mapa.");
            }
            Lines = parlines;
            Height = Lines.Count();
            Length = Lines[0].Length;
            Points = new List<Point>();
            FillAllPoints();
            Position = GetPoint(0, 0);
        }
        public void Print()
        {
            Console.Clear();
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    var point = GetPoint(row, col);
                    Console.ForegroundColor = point.ForegroundColor;
                    Console.BackgroundColor = point.BackgroundColor;
                    Console.Write(point.Value);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public bool Right()
        {
            return Move(0, 1);
        }
        public bool Left()
        {
            return Move(0, -1);
        }
        public bool Up()
        {
            return Move(-1, 0);
        }
        public bool Down()
        {
            return Move(1, 0);
        }
        public bool Move(int rows, int cols)
        {
            var newRow = Position.Row + rows;
            var newCol = Position.Column + cols;
            if (IsInBounds(newRow, newCol))
            {
                Position = GetPoint(newRow, newCol);
                return true;
            }
            return false;
        }

        private void FillAllPoints()
        {
            for (int row = Height - 1; row >= 0; row--)
            {
                for (int col = 0; col < Lines[row].Length - 1; col++)
                {
                    var point = GetPoint(row, col);
                    Points.Add(point);
                }
            }
        }

        public List<Line> GetLinesWhileNotEmpty(Point point)
        {
            var lines = new List<Line>();
            var adyacentes = Adjacents(point.Row, point.Column, 1);
            var ups = adyacentes.Where(p => p.Row == point.Row - 1).ToList();
            if (ups.Any())
            {
                var pointUp = ups.First(p => p.Column == point.Column);
                var lineUp = GetLinePartWhileNotEmpty(pointUp);
                lines.Add(lineUp);
            }
            var lineCenter = GetLinePartWhileNotEmpty(point);
            lines.Add(lineCenter);
            var downs = adyacentes.Where(p => p.Row == point.Row + 1).ToList();
            if (downs.Any())
            {
                var pointDown = downs.First(p => p.Column == point.Column);
                var lineDowns = GetLinePartWhileNotEmpty(pointDown);
                lines.Add(lineDowns);
            }
            return lines;
        }
        public Line GetLinePartWhileNotEmpty(Point point)
        {
            int row = point.Row;
            int startCol = point.Column - 1;
            int endCol = point.Column + 1;

            for (int col = startCol; col >= 0; col--)//Fila Hacia atrás
            {
                if (Lines[row][col] == MapText.EMPTY)
                {
                    break;
                }
                else
                {
                    startCol = col;
                    continue;
                }
            }
            for (int col = endCol; col < Lines[row].Length; col++)//Fila Hacia delante
            {
                if (Lines[row][col] == MapText.EMPTY)
                {
                    break;
                }
                else
                {
                    endCol = col;
                    continue;
                }
            }
            var point1 = GetPoint(row, startCol);
            var point2 = GetPoint(row, endCol);
            var line = GetLine(point1, point2);
            return line;
        }
        public Point GetPoint(int row, int col)
        {
            if (!IsInBounds(row, col))
            {
                throw new Exception("Puntos fuera del intervalo del mapa.");
            }
            var ptr = Lines[row][col];
            return new Point(row, col, ref ptr);
        }
        public Line ReverseLine(Line line)
        {
            var point1 = GetPoint(line.Point2.Row, line.Point2.Column);
            var point2 = GetPoint(line.Point1.Row, line.Point1.Column);
            return GetLine(point1, point2);
        }
        public Line GetLine(Point x, Point y)
        {
            var line = new Line();
            if (x.IsEqual(y))
            {
                var point = GetPoint(x.Row, x.Column);
                line.AddPoint(point);
                return line;
            }
            
            var incRow = x.Row == y.Row ? 0 : x.Row <= y.Row ? 1 : -1;
            var incCol = x.Column == y.Column ? 0 : x.Column <= y.Column ? 1 : -1;

            for (int row = x.Row, col = x.Column; (incRow==1 ? row <= y.Row : row >= y.Row) 
                                               && (incCol == 1 ? col <= y.Column : col >= y.Column)
                ; row+=incRow, col += incCol)
            {
                var point = GetPoint(row, col);
                line.AddPoint(point);
            }            
            return line;
        }

        public List<Point> Adjacents(int row, int startCol, int length)
        {
            var adyacentes = new List<Point>();
            if (!IsInBounds(row, startCol, length))
            {
                throw new Exception("Petición fuera del intervalo.");
            }
            for (int l = 1; l <= length; l++)
            {
                if (l == 1)
                {
                    var adyacentesIzda = AdjacentAllLeft(row, startCol + l - 1);
                    adyacentes.AddRange(adyacentesIzda);
                }

                var adyacentesCentro = AdjacentAllCenter(row, startCol + l - 1);
                adyacentes.AddRange(adyacentesCentro);

                if (l == length)
                {
                    var adyacentesDcha = AdjacentAllRight(row, startCol + l - 1);
                    adyacentes.AddRange(adyacentesDcha);
                }
            }
            return adyacentes;
        }
        public List<Point> AdjacentAllLeft(int row, int col)
        {
            var adyacentes = new List<Point>();            

            for (int r = -1; r <= 1; r++) // Fila adyacente (arriba, misma fila, abajo)
            {
                int c = -1; // Columna adyacente (izquierda)
                int newRow = row + r;
                int newCol = col + c;

                if (IsInBounds(newRow, newCol))
                {
                    var point = GetPoint(newRow, newCol);
                    adyacentes.Add(point);
                }
            }
            return adyacentes;
        }
        public List<Point> AdjacentAllCenter(int row, int col)
        {
            var adyacentes = new List<Point>();

            for (int r = -1; r <= 1; r++) // Fila adyacente (arriba, misma fila, abajo)
            {
                int c = 0; // Columna adyacente (centro)
                if (r == 0)
                {
                    continue; // Saltar la posición propia
                }
                int newRow = row + r;
                int newCol = col + c;
                // Verificar si las coordenadas están dentro del rango
                if (IsInBounds(newRow, newCol ))
                {
                    var point = GetPoint(newRow, newCol);
                    adyacentes.Add(point);
                }
            }
            return adyacentes;
        }
        public List<Point> AdjacentAllRight(int row, int col)
        {
            var adyacentes = new List<Point>();
            for (int r = -1; r <= 1; r++) // Fila adyacente (arriba, misma fila, abajo)
            {
                int c = 1; // Columna adyacente (izquierda)
                int newRow = row + r;
                int newCol = col + c;

                // Verificar si las coordenadas están dentro del rango
                if (IsInBounds(newRow, newCol))
                {
                    var point = GetPoint(newRow, newCol);
                    adyacentes.Add(point);
                }
            }
            return adyacentes;
        }
        public Boolean IsInBounds(Point point)
        {
            return IsInBounds(point.Row, point.Column);

        }
        public Boolean IsInBounds(int row, int col)
        {
            return (row >= 0 && row < Lines.Count() && col >= 0 && col < Lines[row].Length);
            
        }
        public Boolean IsInBounds(int row, int startCol, int len)
        {
            return (row >= 0 || row < Lines.Count() || startCol >= 0 || startCol < Lines[row].Length || (startCol + len) < Lines[row].Length);
        }
        public List<Line> GetRowLines()
        {
            var rowLines = new List<Line>();
            for (int f = 0; f < Height; f++)
            {
                var line = Lines[f];
                var point1 = GetPoint(f, 0);
                var point2 = GetPoint(f, line.Length - 1);
                var recta = GetLine(point1, point2);
                rowLines.Add(recta);
            }
            return rowLines;
        }
        public List<Line> GetColumnLines()
        {
            var colLines = new List<Line>();
            for (int c = 0; c < Lines[0].Length; c++)
            {
                var point1 = GetPoint(0, c);
                var point2 = GetPoint(Height - 1, c);
                var recta = GetLine(point1, point2);
                colLines.Add(recta);
            }
            return colLines;
        }
        public void MarkLine(Line line, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {            
            foreach (var point in line.Points)
            {
                point.ForegroundColor = foregroundColor;
                point.BackgroundColor = backgroundColor;
            }
        }
        public List<Line> GetLinesThroughPoint(Point point)
        {
            var lines = new List<Line>();

            // Línea horizontal
            var horizontalLine = new Line();
            for (int col = 0; col < Length; col++)
            {
                horizontalLine.AddPoint(GetPoint(point.Row, col));
            }
            lines.Add(horizontalLine);

            // Línea vertical
            var verticalLine = new Line();
            for (int row = 0; row < Height; row++)
            {
                verticalLine.AddPoint(GetPoint(row, point.Column));
            }
            lines.Add(verticalLine);

            // Diagonal principal (de arriba a la izquierda a abajo a la derecha)
            var diagonalPrincipal = new Line();
            for (int i = -Math.Min(point.Row, point.Column); i < Math.Min(Height - point.Row, Length - point.Column); i++)
            {
                diagonalPrincipal.AddPoint(GetPoint(point.Row + i, point.Column + i));
            }
            lines.Add(diagonalPrincipal);

            // Diagonal secundaria (de arriba a la derecha a abajo a la izquierda)
            var diagonalSecundaria = new Line();
            for (int i = -Math.Min(point.Row, Length - point.Column - 1); i < Math.Min(Height - point.Row, point.Column + 1); i++)
            {
                diagonalSecundaria.AddPoint(GetPoint(point.Row + i, point.Column - i));
            }
            lines.Add(diagonalSecundaria);

            // Otras líneas en diferentes ángulos
            lines.AddRange(GetAngleLines(point));
            return lines;
        }
        public List<Line> GetAngleLines(Point point)
        {
            // Otras líneas en diferentes ángulos
            var lines = new List<Line>();
            var angles = new List<(int, int)> { (1, 2), (2, 1), (1, -2), (2, -1), (-1, 2), (-2, 1), (-1, -2), (-2, -1) };
            foreach (var (rowInc, colInc) in angles)
            {
                var angleLine = new Line();
                for (int i = 0; ; i++)
                {
                    int newRow = point.Row + i * rowInc;
                    int newCol = point.Column + i * colInc;
                    if (IsInBounds(newRow, newCol))
                    {
                        angleLine.AddPoint(GetPoint(newRow, newCol));
                    }
                    else
                    {
                        break;
                    }
                }
                if (angleLine.Points.Count > 0)
                {
                    lines.Add(angleLine);
                }
            }
            return lines;
        }
        public void SetCharAt(int row, int col, char newChar)
        {
            var lineArray = Lines[row].ToCharArray();
            lineArray[col] = newChar;
            Lines[row] = new string(lineArray);
        }
        public List<Line> GetDiagonals()
        {
            var diagonals = new List<Line>();           
            
            // Obtener diagonales principales (de arriba a la derecha a abajo a la izquierda)
            for (int n = 0; n < Height + Length - 1; n++) //
            {
                var line = new Line();
                for (int col = 0; col <= n; col++)
                {
                    int row = n - col;
                    if (row < Height && col < Length)
                    {
                        var point = GetPoint(row, col);
                        line.AddPoint(point);
                        Console.WriteLine(point.ToString());
                    }
                }
                if (line.Points.Count > 0)
                {
                    diagonals.Add(line);
                }
            }            
            // Obtener diagonales secundarias (de arriba a la izquierda a abajo a la derecha)
            for (int n = 1 - Height; n < Length; n++)
            {
                var line = new Line();
                for (int row = 0; row < Height; row++)
                {
                    int col = row + n;
                    if (col >= 0 && col < Length)
                    {
                        var point = GetPoint(row, col);
                        line.AddPoint(point);
                        Console.WriteLine(point.ToString());
                    }
                }
                if (line.Points.Count > 0)
                {
                    diagonals.Add(line);
                }
            }

            return diagonals;
        }
    }
}

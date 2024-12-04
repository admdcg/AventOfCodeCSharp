using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AventOfCodeCSharp
{
    public class MapText
    {
        public static readonly char EMPTY = '.';
        public List<string> Lines { get; set; }
        public int Height { get; set; }

        public MapText(List<string> parlines)
        {
            Lines = parlines;
            Height = Lines.Count();
        }
        public List<Line> GetLinesWhileNotEmpty(Point point)
        {
            var lines = new List<Line>();
            var adyacentes = Adyacentes(point.Row, point.Column, 1);
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
            return new Point(row, col, Lines[row][col]);
        }
        public Line ReverseLine(Line line)
        {
            var point1 = line.Point2;
            var point2 = line.Point1;
            return GetLine(point1, point2);
        }
        public Line GetLine(Point x, Point y)
        {
            var line = new Line(x, y);
            var incRow = x.Row == y.Row ? 0 : x.Row <= y.Row ? 1 : -1;
            var incCol = x.Column == y.Column ? 0 : x.Column <= y.Column ? 1 : -1;

            for (int row = x.Row, col = x.Column; (incRow==1 ? row <= y.Row : row >= y.Row) 
                                               && (incCol == 1 ? col <= y.Column : col >= y.Column)
                ; row+=incRow, col += incCol)
            {                
                line.Value += Lines[row][col];                
            }
            return line;
        }

        public List<Point> Adyacentes(int row, int startCol, int length)
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
                    var adyacentesIzda = AdyacentesAllIzda(row, startCol + l - 1);
                    adyacentes.AddRange(adyacentesIzda);
                }

                var adyacentesCentro = AdyacentesAllCentro(row, startCol + l - 1);
                adyacentes.AddRange(adyacentesCentro);

                if (l == length)
                {
                    var adyacentesDcha = AdyacentesAllDcha(row, startCol + l - 1);
                    adyacentes.AddRange(adyacentesDcha);
                }
            }
            return adyacentes;
        }
        public List<Point> AdyacentesAllIzda(int row, int col)
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
        public List<Point> AdyacentesAllCentro(int row, int col)
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
        public List<Point> AdyacentesAllDcha(int row, int col)
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
    }
}

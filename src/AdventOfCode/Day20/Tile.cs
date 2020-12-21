using System;
using System.Linq;

namespace AdventOfCode.Day20
{
    internal class Tile
    {
        public int id;
        public int size;
        private readonly string[] Image;
        private int orentation = 0;

        public Tile(int title, string[] image)
        {
            id = title;
            Image = image;
            size = image.Length;
        }

        public void ChangeOrientation()
        {
            orentation++;
            orentation %= 8;
        }

        public char this[int irow, int icol]
        {
            get
            {
                for (var i = 0; i < orentation % 4; i++)
                {
                    (irow, icol) = (icol, size - 1 - irow);
                }

                if (orentation % 8 >= 4)
                {
                    icol = size - 1 - icol;
                }

                return Image[irow][icol];
            }
        }

        public string Row(int irow) => GetSlice(irow, 0, 0, 1);
        public string Top() => GetSlice(0, 0, 0, 1);
        public string Right() => GetSlice(0, size - 1, 1, 0);
        public string Left() => GetSlice(0, 0, 1, 0);
        public string Bottom() => GetSlice(size - 1, 0, 0, 1);

        public override string ToString()
        {
            return $"Tile {id}:{Environment.NewLine}" + string.Join(Environment.NewLine, Enumerable.Range(0, size).Select(i => Row(i)));
        }

        private string GetSlice(int irow, int icol, int drow, int dcol)
        {
            var st = string.Empty;
            for (var i = 0; i < size; i++)
            {
                st += this[irow, icol];
                irow += drow;
                icol += dcol;
            }
            return st;
        }
    }
}

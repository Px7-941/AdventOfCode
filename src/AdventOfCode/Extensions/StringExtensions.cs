namespace AdventOfCode.Extensions
{
    public static class StringExtensions
    {
        public static char? Get(this string[] map, (int, int) pos)
        {
            var (r, c) = pos;
            if (r < 0 || c < 0 || r >= map.Length || c >= map[r].Length)
            {
                return null;
            }

            return map[r][c];
        }
    }
}

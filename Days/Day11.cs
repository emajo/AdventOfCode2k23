class Day11
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day11.txt");
        int currentLine = 0;
        long tot = 0;

        int multiplier = partOne ? 2 : 1000000;

        var positions = new List<Position>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    positions.Add(new Position(i, currentLine));
                }
            }
            currentLine++;

        }
        sr.Close();

        var filledX = positions.GroupBy(p => p.X).Select(p => p.Key).ToArray();
        var emptyX = Enumerable.Range(0, filledX.Max()).Except(filledX).ToArray();

        var filledY = positions.GroupBy(p => p.Y).Select(p => p.Key).ToArray();
        var emptyY = Enumerable.Range(0, filledY.Max()).Except(filledY).ToArray();

        for (int i = 0; i < positions.Count; i++)
        {
            var currentPos = positions[i];
            for (int z = i + 1; z < positions.Count(); z++)
            {
                var maxX = Math.Max(currentPos.X, positions[z].X);
                var minX = Math.Min(currentPos.X, positions[z].X);
                var xToAdd = emptyX.Where(x => x >= minX && x <= maxX).Count();

                var maxY = Math.Max(currentPos.Y, positions[z].Y);
                var minY = Math.Min(currentPos.Y, positions[z].Y);
                var yToAdd = emptyY.Where(y => y >= minY && y <= maxY).Count();

                tot += currentPos.CalculateDistance(positions[z]) + (xToAdd * (multiplier - 1)) + (yToAdd * (multiplier - 1));
            }
        }
        return tot;
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int CalculateDistance(Position comparer)
        {
            return Math.Abs(X - comparer.X) + Math.Abs(Y - comparer.Y);
        }
    }
}
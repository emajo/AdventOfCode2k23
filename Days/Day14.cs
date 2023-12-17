class Day14
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day14.txt");
        int currentLine = 0;

        var roundRocks = new List<Position>();
        var squareRocks = new List<Position>();

        var matrixWidth = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if(matrixWidth == 0) matrixWidth = line.Length;
            squareRocks.Add(new Position(-1, currentLine));
            for (int x = 0; x < line.Length; x++)
            {
                if (currentLine == 0) squareRocks.Add(new Position(x, -1));
                if (line[x] == 'O') roundRocks.Add(new Position(x, currentLine));
                if (line[x] == '#') squareRocks.Add(new Position(x, currentLine));
            }
            squareRocks.Add(new Position(matrixWidth, currentLine));
            currentLine++;

        }

        for (int x = 0; x < matrixWidth; x++)
        {
            squareRocks.Add(new Position(x, currentLine));
        }

        sr.Close();

        var groupedXSquareRocks = squareRocks.GroupBy(r => r.X).ToList();


        if(partOne)
        {
            roundRocks = MoveUp(groupedXSquareRocks, roundRocks, matrixWidth);
            return roundRocks.Sum(r => currentLine - r.Y);
        }

        var groupedYSquareRocks = squareRocks.GroupBy(r => r.Y).ToList();

        int index = 0;

        List<int> results = new List<int>();
        var patternStart = -1;
        var patternLenght = -1;

        while(patternStart == -1)
        {
            roundRocks = MoveUp(groupedXSquareRocks, roundRocks, matrixWidth);
            roundRocks = MoveLeft(groupedYSquareRocks, roundRocks, currentLine);
            roundRocks = MoveDown(groupedXSquareRocks, roundRocks, matrixWidth);
            roundRocks = MoveRight(groupedYSquareRocks, roundRocks, currentLine);

            var res = roundRocks.Sum(r => currentLine - r.Y);

            if(index > 5 && results.Contains(res))
            {
                var possiblePatternEnds = results.Select((p, i) => p == res ? i : -1).Where(i => i != -1).ToArray();
                foreach(var possiblePatternEnd in possiblePatternEnds)
                {
                    var indexDif = index - possiblePatternEnd;

                    if(possiblePatternEnd - indexDif > 0)
                    {
                        if(Enumerable.SequenceEqual(results.GetRange(possiblePatternEnd - indexDif, indexDif), results.GetRange(index - indexDif, indexDif)))
                        {
                            patternStart = possiblePatternEnd - indexDif + 1;
                            patternLenght = indexDif;
                            break;
                        }

                    }

                }

            }

            results.Add(res);
            index++;
      
        }

        return results[patternStart + ((1000000000 - patternStart) % patternLenght) - 1];
    }

    private static List<Position> MoveUp(List<IGrouping<int, Position>> groupedSquareRocks, List<Position> roundRocks, int matrixWidth)
    {
        var groupedRoundRocks = roundRocks.GroupBy(r => r.X).ToList();
        var newRoundRocks = new List<Position>();

        for (int x = 0; x < matrixWidth; x++)
        {
            int maxY = int.MaxValue;
            var xRocks = groupedSquareRocks.Where(r => r.Key == x).ToList();
            if (xRocks.Count == 0) continue;
            foreach (var squareRock in xRocks.First().OrderByDescending(r => r.Y))
            {
                var minY = squareRock.Y;
                var yRocks = groupedRoundRocks.Where(r => r.Key == x).ToList();
                if (yRocks.Count == 0) continue;
                var greaterRoundedRocks = yRocks.First().Where(r => r.Y > minY && r.Y < maxY).ToList();
                for (int i = 0; i < greaterRoundedRocks.Count; i++)
                {
                    newRoundRocks.Add(new Position(x, minY + i + 1));
                }
                maxY = minY;
            }
        }
        
        return newRoundRocks;
    }

    private static List<Position> MoveDown(List<IGrouping<int, Position>> groupedSquareRocks, List<Position> roundRocks, int matrixWidth)
    {
        var groupedRoundRocks = roundRocks.GroupBy(r => r.X).ToList();
        var newRoundRocks = new List<Position>();

        for (int x = 0; x < matrixWidth; x++)
        {
            int minY = -1;
            var xRocks = groupedSquareRocks.Where(r => r.Key == x).ToList();
            if (xRocks.Count == 0) continue;
            foreach (var squareRock in xRocks.First().Where(r => r.Y != -1).OrderBy(r => r.Y))
            {
                var maxY = squareRock.Y;
                var yRocks = groupedRoundRocks.Where(r => r.Key == x).ToList();
                if (yRocks.Count == 0) continue;
                var smallerRoundedRocks = yRocks.First().Where(r => r.Y > minY && r.Y < maxY).ToList();
                for (int i = 0; i < smallerRoundedRocks.Count; i++)
                {
                    newRoundRocks.Add(new Position(x, maxY - i - 1));
                }
                minY = maxY;
            }
        }

        return newRoundRocks;
    }

    private static List<Position> MoveRight(List<IGrouping<int, Position>> groupedSquareRocks, List<Position> roundRocks, int matrixHeight)
    {
        var groupedRoundRocks = roundRocks.GroupBy(r => r.Y).ToList();
        var newRoundRocks = new List<Position>();

        for (int y = 0; y < matrixHeight; y++)
        {
            int maxX = int.MaxValue;
            var yRocks = groupedSquareRocks.Where(r => r.Key == y).ToList();
            if (yRocks.Count == 0) continue;
            foreach (var squareRock in yRocks.First().OrderByDescending(r => r.X))
            {
                var minX = squareRock.X;
                var xRocks = groupedRoundRocks.Where(r => r.Key == y).ToList();
                if (xRocks.Count == 0) continue;
                var greaterRoundedRocks = xRocks.First().Where(r => r.X > minX && r.X < maxX).ToList();
                for (int i = 0; i < greaterRoundedRocks.Count; i++)
                {
                    newRoundRocks.Add(new Position(maxX - i - 1, y));
                }
                maxX = minX;
            }
        }

        return newRoundRocks;
    }

    private static List<Position> MoveLeft(List<IGrouping<int, Position>> groupedSquareRocks, List<Position> roundRocks, int matrixHeight)
    {
        var groupedRoundRocks = roundRocks.GroupBy(r => r.Y).ToList();
        var newRoundRocks = new List<Position>();

        for (int y = 0; y < matrixHeight; y++)
        {
            int minX = -1;
            var yRocks = groupedSquareRocks.Where(r => r.Key == y).ToList();
            if (yRocks.Count == 0) continue;
            foreach (var squareRock in yRocks.First().Where(r => r.X != -1).OrderBy(r => r.X))
            {
                var maxX = squareRock.X;
                var xRocks = groupedRoundRocks.Where(r => r.Key == y).ToList();
                if (xRocks.Count == 0) continue;
                var smallerRoundedRocks = xRocks.First().Where(r => r.X > minX && r.X < maxX).ToList();
                for (int i = 0; i < smallerRoundedRocks.Count; i++)
                {
                    newRoundRocks.Add(new Position(minX + i + 1, y));
                }
                minX = maxX;
            }
        }

        return newRoundRocks;
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

}

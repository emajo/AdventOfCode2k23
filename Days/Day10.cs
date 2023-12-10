class Day10
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day10.txt");
        int currentLine = 0;

        var matrix = new List<List<char>>();
        var currPos = new Position(0, 0);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (currentLine == 0)
                {
                    matrix.Add(new List<char>());
                }
                matrix[i].Add(line[i]);
                if (line[i] == 'S')
                {
                    currPos.Y = currentLine;
                    currPos.X = i;
                }
            }
            currentLine++;

        }
        sr.Close();

        int tot = 0;
        var direction = new Position(0, 0);
        var endPos = new Position(currPos.X, currPos.Y);

        var visitedPos = new List<Position>();

        foreach (var pipe in Entrypoints)
        {
            foreach (var entrypoint in pipe.Value)
            {
                var newX = currPos.X + entrypoint.X;
                var newY = currPos.Y + entrypoint.Y;
                if (
                    matrix[newX][newY] == pipe.Key
                    )
                {
                    currPos.X = newX;
                    currPos.Y = newY;
                    tot++;

                    direction.X = entrypoint.X;
                    direction.Y = entrypoint.Y;

                    break;
                }
            }
            if (tot != 0) break;
        }

        if (!partOne) visitedPos.Add(new Position(currPos.X, currPos.Y));

        do
        {
            var pipe = matrix[currPos.X][currPos.Y];

            var pipeVal = PipeValues[pipe];

            if (direction.Y == 0)
            {
                if (pipeVal.Y != 0)
                {
                    currPos.Y += pipeVal.Y;
                    direction.Y = pipeVal.Y;
                    direction.X = 0;
                }
                else
                {
                    currPos.X += direction.X * pipeVal.X;
                }
            }
            else
            {
                if (pipeVal.X != 0)
                {
                    currPos.X += pipeVal.X;
                    direction.X = pipeVal.X;
                    direction.Y = 0;
                }
                else
                {
                    currPos.Y += direction.Y * pipeVal.Y;
                }
            }
            if (!partOne) visitedPos.Add(new Position(currPos.X, currPos.Y));

            tot++;
        } while (endPos.X != currPos.X || endPos.Y != currPos.Y);

        tot /= 2;

        if(!partOne) {
            var visitedRows = visitedPos.GroupBy(vp => vp.Y).OrderBy(vp => vp.Key).ToArray();
            tot = 0;

            foreach (var visitedRow in visitedRows)
            {
                var row = new List<char>();

                for (int i = 0; i < matrix.Count(); i++)
                {
                    row.Add(matrix[i][visitedRow.Key]);
                }

                var allPosIdx = Enumerable.Range(0, row.Count - 1).ToArray();
                var visitedPosIdx = visitedRow.Select(vr => vr.X).ToArray();
                var notVisitedPos = allPosIdx.Except(visitedPosIdx).ToArray();

                foreach (var nvp in notVisitedPos)
                {
                    row[nvp] = '.';
                    var dirChanges = row.GetRange(0, nvp).Where(p => p == '|' || p == 'J' || p == 'L' || p == 'S').ToArray().Length;
                    if (dirChanges % 2 == 1) tot++;
                }
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

    }

    private static readonly Dictionary<char, Position[]> Entrypoints = new Dictionary<char, Position[]>()
    {
        { '|',  new Position[]
            {
                new Position(0, 1),
                new Position(0, -1),
            }
        },
        { '-',  new Position[]
            {
                new Position(1, 0),
                new Position(-1, 0),
            }
        },
        { 'L',  new Position[]
            {
                new Position(0, 1),
                new Position(-1, 0),
            }
        },
        { 'J',  new Position[]
            {
                new Position(0, 1),
                new Position(1, 0),
            }
        },
        { '7',  new Position[]
            {
                new Position(1, 0),
                new Position(0, -1),
            }
        },
        { 'F',  new Position[]
            {
                new Position(-1, 0),
                new Position(0, -1),
            }
        }
    };

    private static readonly Dictionary<char, Position> PipeValues = new Dictionary<char, Position>()
    {
        { '|',  new Position(0, 1) },
        { '-',  new Position(1, 0) },
        { 'L',  new Position(1, -1) },
        { 'J',  new Position(-1, -1) },
        { '7',  new Position(-1, 1) },
        { 'F',  new Position(1, 1) },
    };

}

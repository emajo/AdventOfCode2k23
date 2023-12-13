class Day13
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day13.txt");
        int currentLine = 0;
        int tot = 0;

        var matrix = new List<List<char>>();

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
            }
            currentLine++;

            if(line == string.Empty)
            {
                var matrixMirrors = CalculateMatrixValue(matrix);
                var matrixVal = matrixMirrors.columns.Sum(c => c * 100) + matrixMirrors.rows.Sum();

                if(!partOne)
                {
                    var found = false;
                    for(int i = 0; i < matrix.Count && !found; i++)
                    {
                        for (int j = 0; j < matrix[i].Count; j++)
                        {
                            var oldCellValue = matrix[i][j];

                            matrix[i][j] = oldCellValue == '#' ? '.' : '#';

                            var newMatrixMirrors = CalculateMatrixValue(matrix);

                            var newColums = newMatrixMirrors.columns.Except(matrixMirrors.columns).ToList();
                            var newRows = newMatrixMirrors.rows.Except(matrixMirrors.rows).ToList();

                            if(newColums.Count > 0 ||  newRows.Count > 0)
                            {
                                found = true;
                                matrixVal = newColums.Sum(c => c * 100) + newRows.Sum();
                                break;
                            }

                            matrix[i][j] = oldCellValue;
                        }

                    }
                }

                tot += matrixVal;
                matrix = new List<List<char>>();
                currentLine = 0;

            }

        }
        sr.Close();
        return tot;
    }

    public static bool IsMirror(List<char> firstSequence, List<char> secondSequence)
    {
        secondSequence.Reverse();
        return Enumerable.SequenceEqual(firstSequence, secondSequence);
    }

    public static List<int> GetPotentialMirrors(List<char> sequence)
    {
        var potentialMirrors = new List<int>();
        for (int j = 0; j < sequence.Count / 2; j++)
        {
            var span = j + 1;
            var first = sequence[..span];
            var second = sequence.GetRange(span, span);
         
            if (IsMirror(first, second)) potentialMirrors.Add(span);

            first = sequence.GetRange(sequence.Count - span * 2, span);
            second = sequence[(sequence.Count - span)..];

            if (IsMirror(first, second)) potentialMirrors.Add(sequence.Count - span);
        }

        return potentialMirrors;

    }

    public static List<int> GetMirrorsToExclude(List<char> sequence, List<int> potentialMirrors)
    {
        var mirrorsToEclude = new List<int>();

        foreach (var potentialMirror in potentialMirrors)
        {
            var span = Math.Min(potentialMirror, sequence.Count - potentialMirror);
            var first = sequence.GetRange(potentialMirror - span, span);
            var second = sequence.GetRange(potentialMirror, span);

            if (!IsMirror(first, second)) mirrorsToEclude.Add(potentialMirror);

        }

        return mirrorsToEclude;
    }

    public static List<int> TestPotentialMirrorsOnMatrix(List<List<char>> matrix, List<int> potentialMirrors, bool isRow = false)
    {
        if (potentialMirrors.Count == 0) return potentialMirrors;

        var sequenceLength = !isRow ? matrix.Count : matrix.First().Count;

        for (int i = 1; i < sequenceLength; i++)
        {
            var sequence = !isRow ? matrix[i] : matrix.Select(m => m[i]).ToList();
            var mirrorsToExclude = GetMirrorsToExclude(sequence, potentialMirrors);
            potentialMirrors = potentialMirrors.Except(mirrorsToExclude).ToList();

        }

        return potentialMirrors;
    }

    public static (List<int> columns, List<int> rows) CalculateMatrixValue(List<List<char>> matrix) {
        var columns = new List<int>();
        var rows = new List<int>();

        var potentialColumnMirrors = GetPotentialMirrors(matrix.First());
        columns = TestPotentialMirrorsOnMatrix(matrix, potentialColumnMirrors);

        var firstRow = matrix.Select(m => m.First()).ToList();
        var potentialRowsMirrors = GetPotentialMirrors(firstRow);
        rows = TestPotentialMirrorsOnMatrix(matrix, potentialRowsMirrors, true);

        return (columns, rows);
    }

}

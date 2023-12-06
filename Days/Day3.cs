class Day3
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day3.txt");
        int currentLine = 0;

        var matrix = new List<List<char>>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() + '.';

            for (int i = 0; i < line.Length; i++)
            {
                if(currentLine == 0)
                {
                    matrix.Add(new List<char>());
                }
                matrix[i].Add(line[i]);
            }
            currentLine++;

        }
        sr.Close();

        int tot = 0;
        var starPositions = new List<(int x, int y, int val)>();
        var checkFound = (int res) => res > -1;

        for(int y = 0; y < matrix.Count() - 1; y ++)
        {
            string num = string.Empty;
            for(int x = 0; x <= matrix[y].Count(); x ++)
            {
                if (char.IsNumber(matrix[x][y])) num += matrix[x][y];
                else
                {
                    if(num != "")
                    {
                        if(partOne)
                        {
                            bool found = checkFound(checkX(matrix, y - 1, x - num.Length - 1, x)) ||
                                checkFound(checkX(matrix, y + 1, x - num.Length - 1, x)) ||
                                checkFound(checkY(matrix, x - num.Length - 1, y - 1, y + 1)) ||
                                checkFound(checkY(matrix, x, y - 1, y + 1));


                            if(found)
                            {
                                tot += Convert.ToInt16(num);
                            }
                        } else
                        {
                            var star = checkStars(matrix, x, y, num.Length);

                            if(star.x != -1 && star.y != -1) {
                                var posVal = starPositions.Find((pos) => pos.x == star.x && pos.y == star.y);
                                if (posVal != (0, 0, 0))
                                {
                                    tot += Convert.ToInt16(num) * posVal.val;
                                    // Console.WriteLine(num + " - " + posVal.val);
                                } else
                                {
                                    starPositions.Add((star.x, star.y, Convert.ToInt16(num)));
                                }
                            }

                        }

                        num = "";
                    }
                }
            }

        }

        return tot;
    }

    private static int checkX(List<List<char>> matrix, int y, int xFrom, int xTo, bool onlyStars = false)
    {
        if(y < 0 || y >= matrix.Last().Count()) return -1;
        for(int x = xFrom; x <= xTo; x++)
        {
            if (x < 0 || x >= matrix.Count()) continue;
            if(checkPoint(matrix, x, y, onlyStars)) return x;
        }
        return -1;
    }

    private static int checkY(List<List<char>> matrix, int x, int yFrom, int yTo, bool onlyStars = false)
    {
        if (x < 0 || x >= matrix.Count()) return -1;
        for (int y = yFrom; y <= yTo; y++)
        {
            if (y < 0 || y >= matrix.Last().Count()) continue;
            if (checkPoint(matrix, x, y, onlyStars)) return y;
        }
        return -1;
    }

    private static bool checkPoint(List<List<char>> matrix, int x, int y, bool onlyStars = false)
    {
        return !onlyStars ? !char.IsNumber(matrix[x][y]) && matrix[x][y] != '.' : matrix[x][y] == '*';
    }

    private static (int x, int y) checkStars(List<List<char>> matrix, int x, int y, int numLength)
    {
        var topStar = checkX(matrix, y - 1, x - numLength, x - 1, true);
        if (topStar > -1) return (topStar, y - 1);

        var bottomStar = checkX(matrix, y + 1, x - numLength, x - 1, true);
        if (bottomStar > -1) return (bottomStar, y + 1);

        var rightStar = checkY(matrix, x, y - 1, y + 1, true);
        if (rightStar > -1) return (x, rightStar);

        var leftStar = checkY(matrix, x - numLength - 1, y - 1, y + 1, true);
        if (leftStar > -1) return (x - numLength -1, leftStar);

        return (-1, -1);
    }

}

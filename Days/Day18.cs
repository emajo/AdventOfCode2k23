class Day18
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day18.txt");

        var positions = new List<ColorfulPosition>();

        positions.Add(new ColorfulPosition(0, 0));
        var curr = 0;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var splittedLine = line.Split(' ');
            
            var direction = (Direction)splittedLine[0].First();
            var lenght = Convert.ToInt32(splittedLine[1]);
            var color = splittedLine[2].Substring(1, splittedLine[2].Length - 2);

            if(!partOne)
            {
                switch(color.Last())
                {
                    case '0': direction = Direction.Right; break;
                    case '1': direction = Direction.Down; break;
                    case '2': direction = Direction.Left; break;
                    case '3': direction = Direction.Up; break;
                }

                lenght = Convert.ToInt32(color.Substring(1, color.Length - 2), 16);
            }
            
            var newPos = ColorfulPosition.Move(positions.Last(), direction, lenght, color);
            positions.AddRange(newPos);
            curr++;
            if (curr % 50 == 0) Console.WriteLine(curr);

        }
        sr.Close();

        //for(int x = -100; x < 100; x++)
        //{
        //   for (int y = -100; y < 100; y++)
        //   {
        //       if(positions.Where(p => p.X == y && p.Y == x).FirstOrDefault() == null)
        //       {
        //           Console.Write(".");
        //       } else                 Console.Write("#");

        //   }

        //   Console.Write('\n');
        //}

        positions.RemoveAt(positions.Count-1);

        //var yPositions = positions.GroupBy(p => p.Y).OrderBy(p => p.Key).ToArray();
        //var tot = 0;
        //foreach(var yPos in yPositions)
        //{
        //    var yPosList = yPos.OrderBy(p => p.X).Select(p => p.X).ToArray();
        //    var tempTot = 0;
        //    for (int x = 1; x < yPosList.Length; x++)
        //    {
        //        Console.WriteLine(yPosList[x] + " " + yPosList[x - 1] + ": " + (yPosList[x] - yPosList[x - 1]));
        //        tempTot += yPosList[x] - yPosList[x - 1];
        //    }

        //    Console.WriteLine("key: " + yPos.Key + " tot: " + tempTot);
        //    tot += tempTot + 1;
        //}
        

        return (long)Area(positions) + 2;
    }

    private static double Area(List<ColorfulPosition> positions)
    {
        var sum = 0.0;
        for (var i = 0; i < positions.Count; i++)
        {
            var a = positions[i];
            var b = positions[(i + 1) % positions.Count];
            sum += (b.X + a.X) * (b.Y - a.Y) + 1;
        }
        return sum / 2 - 1;

    }


    class ColorfulPosition
    {
        public long X, Y;
        public string Color;
        public ColorfulPosition(long x, long y, string color = "")
        {
            X = x;
            Y = y;
            Color = color;
        }

        public static List<ColorfulPosition> Move(ColorfulPosition startPos, Direction direction, int steps, string color) 
        { 
            var visitedPositions = new List<ColorfulPosition>();

            long x = startPos.X;
            long y = startPos.Y;

            for (int i = 0; i < steps; i++)
            {
                switch(direction)
                {
                    case Direction.Up: y--; break;
                    case Direction.Down: y++; break;
                    case Direction.Left: x--; break;
                    case Direction.Right: x++; break;
                }
                // da ottimizzare, non c'è bisogno di aggiungere tutti i punti
                visitedPositions.Add(new ColorfulPosition(x, y, color));
            }

            return visitedPositions;
        }
    }

    enum Direction
    {
        Up = 'U',
        Down = 'D',
        Left = 'L',
        Right = 'R',
    }

}

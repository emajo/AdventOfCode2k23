class Day17
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day17.txt");
        int currentLine = 0;

        var matrix = new List<List<int>>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (currentLine == 0)
                {
                    matrix.Add(new List<int>());
                }
                matrix[i].Add((int)char.GetNumericValue(line[i]));
            }
            currentLine++;

        }
        sr.Close();


        return calculateShortesPath(
            matrix,
            new List<Movement>()
            {
               new Movement(
                   new Position(0, 0),
                   Direction.Right,
                   0,
                   matrix[0][0]
               ),
                 new Movement(
                   new Position(0, 0),
                   Direction.Down,
                   0,
                   matrix[0][0]
               ),
            },
            !partOne
        );
    }

    private static int calculateShortesPath(List<List<int>> matrix, List<Movement> startMovements, bool ultra)
    {
        var pastMovements = new HashSet<string>();

        var movementQueue = new PriorityQueue<Movement, int>();
        startMovements.ForEach(
            m => movementQueue.Enqueue(m, m.Heat)
        );

        var minHeat = 0;

        while (movementQueue.Count > 0)
        {
            // dequeue movement
            var movement = movementQueue.Dequeue();

            // check if already visited
            var movementKey = $"{movement.Position.X}-{movement.Position.Y}-{movement.Direction}-{movement.Streak}";
            if (pastMovements.Contains(movementKey)) continue;
            else pastMovements.Add(movementKey);

            // move position
            movement.Move();

            // check matrix bounds
            if (
                movement.Position.ExceedsMatrixBounds(matrix)
            ) continue;

            // increment heat
            movement.Heat += matrix[movement.Position.X][movement.Position.Y];

            // check if arrived at the end
            if (movement.Position.X == matrix.Count - 1 && movement.Position.Y == matrix.First().Count - 1)
            {
                minHeat = movement.Heat;
                break;
            }

            // calculate and enqueue new movements
            movement.CalculateNextMovements(ultra).ForEach(
                m => movementQueue.Enqueue(m,  m.Heat)
            );
        }

        return minHeat - matrix[0][0];
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool ExceedsMatrixBounds(List<List<int>> matrix)
        {
            return X < 0
             || Y < 0
             || X >= matrix.Count
             || Y >= matrix.First().Count;
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    class Movement
    {
        public Position Position;
        public Direction Direction;
        public int Streak;
        public int Heat;

        public Movement(Position position, Direction direction, int streak = 0, int heat = 0)
        {
            Position = position;
            Direction = direction;
            Streak = streak;
            Heat = heat;
        }

        public void Move()
        {
            Position = NextPosition(Position, Direction);
            Streak++;
        }

        public static Position NextPosition(Position position, Direction direction)
        {
            var newPos = new Position(position.X, position.Y);
            switch (direction)
            {
                case Direction.Left:
                    newPos.X--;
                    break;
                case Direction.Right:
                    newPos.X++;
                    break;
                case Direction.Up:
                    newPos.Y--;
                    break;
                case Direction.Down:
                    newPos.Y++;
                    break;
            }

            return newPos;
        }

        public List<Movement> CalculateNextMovements(bool ultra)
        {
            var possibleDirections = new List<Direction>
            {
               Direction.Up,
               Direction.Down,
               Direction.Left,
               Direction.Right,
            };

            switch(Direction)
            {
                case Direction.Up:
                    possibleDirections.Remove(Direction.Down); break;
                case Direction.Down:
                    possibleDirections.Remove(Direction.Up); break;
                case Direction.Left:
                    possibleDirections.Remove(Direction.Right); break;
                case Direction.Right:
                    possibleDirections.Remove(Direction.Left); break;
            }
            if (ultra) {
                if (Streak < 4) possibleDirections = new List<Direction> { Direction };
                if (Streak == 10) possibleDirections.Remove(Direction);
            }
            else if (Streak == 3) possibleDirections.Remove(Direction);

            return possibleDirections.Select(d => new Movement(new Position(Position.X, Position.Y), d, d == Direction ? Streak : 0, Heat)).ToList();
        }
    }
}

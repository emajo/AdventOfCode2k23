class Day16
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day16.txt");
        int currentLine = 0;

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

        }
        sr.Close();


        if (partOne)
        {
            return calculateVisitedPositions(
                matrix, 
                new Movement(
                    new Position(-1, 0),
                    Direction.Right
                ));
        }

        int maxPositions = -1;
        int matrixWidth = matrix.Count;
        int matrixHeight = matrix.First().Count;

        Parallel.For(0, matrixWidth,
            x =>
            {
                var maxXPos = Math.Max(
                    calculateVisitedPositions(
                        matrix,
                        new Movement(
                            new Position(x, -1),
                            Direction.Down
                        )),
                    calculateVisitedPositions(
                        matrix,
                        new Movement(
                            new Position(x, matrixHeight),
                            Direction.Up
                        ))
                );
                maxPositions = Math.Max(maxPositions, maxXPos);
            });

        Parallel.For(0, matrixWidth,
            y => {
                var maxYPos = Math.Max(
                    calculateVisitedPositions(
                        matrix,
                        new Movement(
                            new Position(-1, y),
                            Direction.Right
                        )),
                    calculateVisitedPositions(
                        matrix,
                        new Movement(
                            new Position(matrixWidth, y),
                            Direction.Left
                        ))
                );
                maxPositions = Math.Max(maxPositions, maxYPos);
            });

        return maxPositions;
    }

    private static int calculateVisitedPositions(List<List<char>> matrix, Movement startMovement)
    {
        var visitedPositions = new HashSet<string>();
        var pastMovements = new HashSet<string>();

        var movementQueue = new Queue<Movement>();
        movementQueue.Enqueue(startMovement);

        while (movementQueue.Count > 0)
        {
            // dequeue movement
            var movement = movementQueue.Dequeue();

            // cycle detection
            var movementKey = $"{movement.Position.X}-{movement.Position.Y}-{movement.Direction}";
            if (pastMovements.Contains(movementKey)) continue;
            else pastMovements.Add(movementKey);

            // move position
            movement.Move();

            // check matrix bounds
            if (
                movement.Position.X < 0
                || movement.Position.Y < 0
                || movement.Position.X >= matrix.Count
                || movement.Position.Y >= matrix.First().Count
            ) continue;

            // add visited position
            var positionKey = $"{movement.Position.X}-{movement.Position.Y}";
            visitedPositions.Add(positionKey);

            // calculate and enqueue new movements
            char instruction = matrix[movement.Position.X][movement.Position.Y];
            movement.CalculateNextMovements((Instruction)instruction).ForEach(
                movementQueue.Enqueue
            );
        }

        return visitedPositions.Count;
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

    enum Instruction
    {
        Space = '.',
        ForwardMirror = '/',
        ReverseMirror = '\\',
        PipeSplitter = '|',
        DashSplitter = '-',
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

        public Movement(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public void Move()
        {
            switch(Direction)
            {
                case Direction.Left:
                    Position.X--;
                    break;
                case Direction.Right:
                    Position.X++;
                    break;
                case Direction.Up:
                    Position.Y--;
                    break;
                case Direction.Down:
                    Position.Y++;
                    break;
            }
        }

        public List<Movement> CalculateNextMovements(Instruction instruction)
        {
            var nextDirections = new List<Direction>();

             switch(instruction)
            {
                case Instruction.Space:
                    nextDirections.Add(Direction);
                    break;
                case Instruction.ForwardMirror:
                    switch(Direction)
                    {
                        case Direction.Left:
                            nextDirections.Add(Direction.Down);
                            break;
                        case Direction.Right:
                            nextDirections.Add(Direction.Up);
                            break;
                        case Direction.Up:
                            nextDirections.Add(Direction.Right);
                            break;
                        case Direction.Down:
                            nextDirections.Add(Direction.Left);
                            break;
                    }
                    break;
                case Instruction.ReverseMirror:
                    switch (Direction)
                    {
                        case Direction.Left:
                            nextDirections.Add(Direction.Up);
                            break;
                        case Direction.Right:
                            nextDirections.Add(Direction.Down);
                            break;
                        case Direction.Up:
                            nextDirections.Add(Direction.Left);
                            break;
                        case Direction.Down:
                            nextDirections.Add(Direction.Right);
                            break;
                    }
                    break;
                case Instruction.DashSplitter:
                    switch (Direction)
                    {
                        case Direction.Left:
                        case Direction.Right:
                            nextDirections.Add(Direction);
                            break;
                        case Direction.Up:
                        case Direction.Down:
                            nextDirections.Add(Direction.Right);
                            nextDirections.Add(Direction.Left);
                            break;
                    }
                    break;
                case Instruction.PipeSplitter:
                    switch (Direction)
                    {
                        case Direction.Left:
                        case Direction.Right:
                            nextDirections.Add(Direction.Up);
                            nextDirections.Add(Direction.Down);
                            break;
                        case Direction.Up:
                        case Direction.Down:
                            nextDirections.Add(Direction);
                            break;
                    }
                    break;
            }

            return nextDirections.Select(d => new Movement(new Position(Position.X, Position.Y), d)).ToList();
        }
    }
}

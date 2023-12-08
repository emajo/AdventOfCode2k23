using System.Numerics;

class Day8
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day8.txt");

        var map = new Dictionary<string, Direction>();

        string instructions = sr.ReadLine() ?? "";
        sr.ReadLine();

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            var splittedLine = line.Split(" = ");

            var instr = splittedLine[1].Substring(1, splittedLine[1].Length - 2).Split(", ");

            map.Add(splittedLine[0], new Direction(instr[0], instr[1]));

        }

        sr.Close();

        long tot = 0;

        if (partOne)
        {
            string element = "AAA";

            for (int i = 0; i < instructions.Length; i++)
            {
                element = map[element].GoTo(instructions[i]);
                tot++;
                if (element == "ZZZ") break;
                if (i == instructions.Length - 1) i = -1;
            }

        } else
        {
            var elements = map.Where(m => m.Key.EndsWith('A')).Select(m => m.Key).ToArray();
            var loops = new Dictionary<string, long>();

            int index = 0;
            while (loops.Count() != elements.Count()) {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (loops.ContainsKey(elements[i])) continue;
                    elements[i] = map[elements[i]].GoTo(instructions[(index) % instructions.Length]);
                    if (elements[i].EndsWith('Z'))
                    {
                        loops.Add(elements[i], index + 1);
                    }
                }
                    index++;
            }

            tot = (ArrayLCM(loops.Select(l => l.Value).ToArray()));

        }

        return tot;
    }

    class Direction
    {
        public string Left, Right;

        public Direction(string left, string right)
        {
            Left = left;
            Right = right;
        }

        public string GoTo(char direction)
        {
            if (direction == 'R') return Right;
            return Left;
        }
    }

    public static long ArrayLCM(long[] numbers)
    {
        return numbers.Aggregate(LCM);
    }
    public static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    public static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }


}

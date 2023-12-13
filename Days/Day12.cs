class Day12
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day12.txt");

        long tot = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            var splittedLine = line.Split(' ');

            var springs = splittedLine[0];
            var groups = splittedLine[1].Split(",").Select(int.Parse).ToArray();
            if (!partOne)
            {
                springs = string.Join('?', Enumerable.Repeat(springs, 5));
                groups = Enumerable.Repeat(groups, 5).SelectMany(g => g).ToArray();
            }

            tot += new Day12().Count(springs, groups);

        }
        sr.Close();

        return tot;
    }

    private Dictionary<string, long> cache = new Dictionary<string, long>();

    public long Count(string springs, int[] groups)
    {
        if(cache.TryGetValue(springs + string.Join("-", groups), out long val)) {
            return val;
        }

        long count = 0;

        if(springs == string.Empty)
        {
            return groups.Length == 0 ? 1 : 0;
        }

        if(groups.Length == 0)
        {
            return !springs.Contains('#') ? 1 : 0;
        }

        if (springs.First() == '.' || springs.First() == '?')
            count += Count(springs[1..], groups);

        if (springs.First() == '#' || springs.First() == '?')
        {
            if (groups.First() <= springs.Length
                && !springs.Substring(0, groups.First()).Contains('.')

                )
            {
                if (
                    springs.Length == groups.First()
                    ) count += Count("", groups[1..]);
                else if (springs[groups.First()] == '.' || springs[groups.First()] == '?')
                    count += Count(springs[(groups.First() + 1)..], groups[1..]);
            }
        }

        cache.Add(springs + string.Join("-", groups), count);

        return count;
    }


}

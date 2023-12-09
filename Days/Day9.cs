
class Day9
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day9.txt");
        long tot = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            var values = line.Split(" ").Select(long.Parse).ToList();

            if(!partOne) values.Reverse();

            while(values.Exists(v => v != 0))
            {
                tot += values.Last();
                var newValues = new List<long>();
                for (int i = 1; i < values.Count; i++)
                {
                    newValues.Add(values[i] - values[i - 1]);
                }
                values = newValues;
            }

        }

        sr.Close();

        return (int)tot;
    }
}

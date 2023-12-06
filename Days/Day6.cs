class Day6
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day6.txt");

        var times = new List<long>();
        var distances = new List<long>();

        var tempTimes = sr.ReadLine().Split(":")[1].Split(" ").Where((t) => t != "");
        var tempDistances = sr.ReadLine().Split(":")[1].Split(" ").Where((t) => t != "");

        if(partOne)
        {
            times = tempTimes.Select(long.Parse).ToList();
            distances = tempDistances.Select(long.Parse).ToList();
        } else
        {
            times.Add(Convert.ToInt64(string.Concat(tempTimes.ToArray())));
            distances.Add(Convert.ToInt64(string.Concat(tempDistances.ToArray())));
        }

        double tot = 1;

        for(int i = 0; i < times.Count(); i++)
        {
            var min = Math.Floor(calculateIntersection(times[i], distances[i], true));
            var max = Math.Ceiling(calculateIntersection(times[i], distances[i], false));
            tot *= (max - min  - 1);
        }

        sr.Close();

        return (int)tot;
    }

    public static double calculateIntersection(long time, long distance, bool min)
    {
        var delta = Math.Sqrt(Math.Pow(time, 2) - (4 * distance));

        delta *= min ? -1 : 1;

        return (-time + delta) / 2;
    }


}

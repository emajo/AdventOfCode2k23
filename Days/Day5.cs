class Day5
{
    public static int PartOne()
    {
        var sr = new StreamReader("inputs/day5.txt");

        var seeds = sr.ReadLine()?.Split(": ")[1].Split(" ")?.Select(long.Parse)?.ToList();

        sr.ReadLine();

        var checkedLocations = new List<long>();

        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine();
            if(line == "")
            {
                continue;
            }

            if(line.Contains("map"))
            {
                checkedLocations.Clear();
                continue;
            }

            // normal line
            var splittedLine = line.Split(" ").Select(long.Parse).ToArray();

            var mapEntry = new MapEntry(splittedLine[1], splittedLine[0], splittedLine[2]);

   
            Parallel.For(0, seeds.Count(),
             i => {
                 if (!checkedLocations.Contains(i))
                 {
                 var location = mapEntry.CalculateLocation(seeds[i]);
                 if (location != seeds[i]) checkedLocations.Add(i);
                 seeds[i] = location;

                 }
             });

        }

        sr.Close();

        return (int)seeds.Min();
    }

    public static int PartTwo()
    {
        var sr = new StreamReader("inputs/day5.txt");
        int mapCount = 0;
        var maps = new List<List<MapEntry>>();

        var seeds = sr.ReadLine()?.Split(": ")[1].Split(" ")?.Select(long.Parse)?.ToList();
        sr.ReadLine();

      
            var compareList = new List<Compare>();
            for (int z = 0; z <= seeds.Count() / 2; z += 2)
            {
                compareList.Add(new Compare(seeds[z], seeds[z + 1]));
            }
        

        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine();
            if (line == "")
            {
                mapCount++;
                continue;
            }

            if (line.Contains("map"))
            {
                maps.Add(new List<MapEntry>());
                continue;
            }

            // normal line
            var splittedLine = line.Split(" ").Select(long.Parse).ToArray();

            var mapEntry = new MapEntry(splittedLine[1], splittedLine[0], splittedLine[2]);
            maps[mapCount].Add(mapEntry);
        }

        long toCheck = 0;
        bool found = false;
        while(!found)
        {
            long foundLocation = -1;
            for(int i = maps.Count() - 1; i >= 0; i--)
            {
                var map = maps[i];
                
                foreach(var me in map)
                {
                    var curr = foundLocation != -1 ? foundLocation : toCheck;
                    var loc = me.ReverseCalculateLocation(curr);
                    if (loc == curr) continue;
                    foundLocation = loc;
                    break;

                }
                if (foundLocation == -1) continue;
                
            }

            found = compareList.Exists((cl) => foundLocation >= cl.Min && foundLocation <= cl.Min + cl.Range);
            
            toCheck++;
        }

        return (int)toCheck - 1;

    }

    struct Compare
    {
        public long Min, Range;

        public Compare(long min, long range)
        {
            Min = min;
            Range = range;
        }
    }

    class MapEntry
    {
        public long SourceStart, DestinatonStart, Range;
        public MapEntry(long sourceStart, long destinationStart, long range)
        {
            SourceStart = sourceStart;
            DestinatonStart = destinationStart;
            Range = range;
        }

        private bool contains(long seed)
        {
            return seed >= SourceStart && seed <= SourceStart + Range;
        }

        public long CalculateLocation(long seed)
        {
            if (!contains(seed)) return seed;
            return DestinatonStart + (seed - SourceStart);
        }

        private bool reverseContains(long seed)
        {
            return seed >= DestinatonStart && seed <= DestinatonStart + Range;
        }

        public long ReverseCalculateLocation(long seed)
        {
            if (!reverseContains(seed)) return seed;
            return SourceStart + (seed - DestinatonStart);
        }

    }

}

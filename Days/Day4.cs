class Day4
{
    public static int PartOne()
    {
        var sr = new StreamReader("inputs/day4.txt");
        int tot = 0;


        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine() ?? "";
            string card = line.Split(": ")[1];
            string[] numLists = card.Split(" | ");

            string[] winningNums = numLists[0].Split(" ");
            string[] nums = numLists[1].Split(" ");

            int winsCount = 0;

            foreach(string num in nums)
            {
                if (num == "") continue;
                 if(winningNums.Contains(num)) winsCount++;
            }
            if (winsCount != 0) tot += (int)Math.Pow(2,  (winsCount - 1));
        }

        sr.Close();

        return tot;
    }

    public static int PartTwo()
    {
        var sr = new StreamReader("inputs/day4.txt");
        int tot = 0;


        List<int> wonCards = new List<int>();
        int lineIdx = 0;

        while (!sr.EndOfStream)
        {
            if (wonCards.Count() - 1 < lineIdx) wonCards.Add(0);
            wonCards[lineIdx]++;

            string line = sr.ReadLine() ?? "";
            string card = line.Split(": ")[1];
            string[] numLists = card.Split(" | ");

            string[] winningNums = numLists[0].Split(" ");
            string[] nums = numLists[1].Split(" ");

            int winsCount = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (num == "") continue;
                if (winningNums.Contains(num))
                {
                    winsCount++;
                    for (int z = wonCards.Count(); z <= winsCount + lineIdx; z++) wonCards.Add(0);
                    wonCards[lineIdx + winsCount] += wonCards[lineIdx];
                }
            }
            lineIdx++;
        }

        tot = wonCards.Sum();
        sr.Close();

        return tot;
    }

}

class Day1
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader(@"inputs/day1.txt");

        int tot = 0;

        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine() ?? "";
            // Console.WriteLine(line);
            int foundIndex = line.Length;
            var (num, index) = findDigitNums(line);

            if(num > -1)
            {
                foundIndex = index;
            }

            if(!partOne)
            {
                string toCheckForStringNums = line.Substring(0, foundIndex);
                int foundStrNum = findStringNums(toCheckForStringNums);
                if (foundStrNum > -1) num = foundStrNum;
            }
            // Console.WriteLine(num);
            tot += num * 10;

            foundIndex = 0;
            var (lastNum, lastIndex) = findDigitNums(line, true);

            if (lastNum > -1)
            {
                foundIndex = lastIndex;
            }

            if (!partOne)
            {
                string toCheckForStringNums = line.Substring(foundIndex);
                int foundStrNum = findStringNums(toCheckForStringNums, true);
                if (foundStrNum > -1) lastNum = foundStrNum;
            }
            // Console.WriteLine(lastNum);
            tot += lastNum;

        }
        return tot;
        sr.Close();
    }

    private static (int num, int index) findDigitNums(string toCheck, bool findLast = false)
    {
        int foundIndex = findLast ? 0 : toCheck.Length;
        int num = -1;

        for (int i = 0; i < toCheck.Length; i++)
        {
            int indexToCheck = findLast ? toCheck.Length - 1 - i : i;
            char c = toCheck[indexToCheck];
            if (char.IsDigit(c))
            {
                num = (int)char.GetNumericValue(c);
                foundIndex = indexToCheck;
                break;
            }
        }

        return (num, foundIndex);
    }

    private static int findStringNums(string toCheck, bool findLast = false)
    {
        string[] strNums = {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        int foundNum = -1;

        for (int i = 0; i < strNums.Length; i++)
        {
            int index = findLast ? toCheck.LastIndexOf(strNums[i]) : toCheck.IndexOf(strNums[i]);
            if (index > -1)
            {
                foundNum = i + 1;
                toCheck = findLast ? toCheck.Substring(index) : toCheck.Substring(0, index + strNums[i].Length);
            }
        }

        return foundNum;
    }

}

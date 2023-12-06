class Day2
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader(@"inputs/day2.txt");

        int tot = 0;
        int index = 1;

        RGB max = new RGB(12, 13, 14);

        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine() ?? "";
            string cubes = line.Split(": ")[1];
            string[] sets = cubes.Split("; ");

            RGB comp = new RGB();
            RGB min = new RGB();
            bool isPossible = true;

            foreach (string set in sets)
            {
                string[] amounts = set.Split(", ");
                foreach (string amount in amounts)
                {
                    string[] splittedAmount = amount.Split(" ");
                    comp.SetValueByColor(splittedAmount[1], Convert.ToInt16(splittedAmount[0]));
                }
                if (partOne)
                {
                    if (!comp.IsSmaller(max))
                    {
                        isPossible = false;
                        break;
                    }

                } else 
                {
                    if(min.R == 0 || min.R < comp.R) min.R = comp.R;
                    if(min.G == 0 || min.G < comp.G) min.G = comp.G;
                    if(min.B == 0 || min.B < comp.B) min.B = comp.B;
                }
            }
            if(partOne)
            {
                if (isPossible) tot += index;
            } else
            {
                tot += min.R * min.G * min.B;
            }
            index++;
        }
        return tot;
        sr.Close();
    }
     class RGB
    {
        public int R;
        public int G;
        public int B;
        public RGB(int r = 0, int g = 0 , int b = 0)
        {
            R = r;
            G = g;
            B = b;
        }

        public RGB SetValueByColor(string color, int value)
        {
            switch(color)
            {
                case "red": R = value;
                    break;
                case "green": G = value;
                    break;
                case "blue": B = value;
                    break;
            }

            return this;
        }

        public bool IsSmaller(RGB comp)
        {
            return R <= comp.R && G <= comp.G && B <= comp.B;
        }
    }

}

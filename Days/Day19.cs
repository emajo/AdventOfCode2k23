using System.Numerics;

class Day19
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day19.txt");

        var workflows = new Dictionary<string, List<Condition>>();
        var tot = 0;

        var readingWorkflows = true;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            if(line == "")
            {
                readingWorkflows = false;
                continue;
            }

            if(readingWorkflows)
            {
                var splittedLine = line.Substring(0, line.Length - 1).Split('{');
                var key = splittedLine[0];
                var conditions = splittedLine[1].Split(',');

                workflows.Add(key, new List<Condition>());

                foreach( var condition in conditions )
                {
                    var field = ' ';
                    var op = ' ';
                    var value = 0;
                    var result = "";
                    if (condition.Contains(':'))
                    {
                        var splittedCondition = condition.Split(':');

                        result = splittedCondition[1];
              

                        if (splittedCondition[0].Contains('<'))
                        {
                            field = splittedCondition[0].Split('<')[0].First();
                            op = '<';
                            value = Convert.ToInt16(splittedCondition[0].Split('<')[1]);
                        } else
                        {
                            field = splittedCondition[0].Split('>')[0].First();
                            op = '>';
                            value = Convert.ToInt16(splittedCondition[0].Split('>')[1]);
                        }
               
                    } else
                    {
                        result = condition;
                    }
                    workflows[key].Add(
                       new Condition(
                           field, op, value, result
                       )
                    );
                }
            } else
            {
                var values = line.Substring(1, line.Length - 2).Split(',');
                var x = Convert.ToInt16(values[0].Split('=')[1]);
                var m = Convert.ToInt16(values[1].Split('=')[1]);
                var a = Convert.ToInt16(values[2].Split('=')[1]);
                var s = Convert.ToInt16(values[3].Split('=')[1]);

                var rating = new Rating(x, m, a, s);

                var val = CalculateValue(rating, workflows, "in");
                tot += val;
            }
 
        }
        sr.Close();
        return tot;
    }

    private static int CalculateValue(Rating rating, Dictionary<string, List<Condition>> workflows, string workflowKey)
    {
        foreach (var condition in workflows[workflowKey])
        {
            if(rating.Passes(condition))
            {
                if(condition.Result == "A") return rating.X + rating.M + rating.A + rating.S;
                if(condition.Result == "R") return 0;

                return CalculateValue(rating, workflows, condition.Result);
            }
        }

        return 0;
    }

    class Rating
    {
        public int X, M, A, S;
        public Rating(int x, int m, int a, int s)
        {
            X = x;
            M = m;
            A = a;
            S = s;
        }

        public bool Passes(Condition condition)
        {
            int value;
            switch (condition.Field)
            {
                case 'x': value = X; break;
                case 'm': value = M; break;
                case 'a': value = A; break;
                case 's': value = S; break;
                default: return true;
            }

            if(condition.Op == '<')
            {
                return value < condition.Value;
            } 
            return value > condition.Value;
        }
    }

    class Condition
    {
        public char Field, Op;
        public int Value;
        public string Result;

        public Condition(char field, char op, int value, string result)
        {
            Field = field;
            Op = op;
            Value = value;
            Result = result;
        }

    }
}

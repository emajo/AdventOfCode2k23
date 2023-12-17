class Day15
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day15.txt");
        
        var line = sr.ReadLine() + ',';
        sr.Close();

        var tot = 0;

        var currentString = "";
        var boxes = new Dictionary<int, List<Lens>>();

        for (int i = 0; i < line.Length; i++)
        {
            if(line[i] != ',')
            {
                currentString += line[i];
                continue;
            }

            if(partOne)
            {
                var value = calculateHash(currentString);
                tot += value;
            } else
            {
                if (currentString.Contains('-')) {

                    var label = currentString.Split('-')[0];
                    var box = calculateHash(label);

                    if (boxes.ContainsKey(box))
                        boxes[box].RemoveAll(l => l.Label == label);
                }
                else
                {
                    var lens = new Lens(
                        currentString.Split('=')[0],
                        Convert.ToInt16(currentString.Split('=')[1])
                    );

                    var box = calculateHash(lens.Label);

                    if (!boxes.ContainsKey(box)) boxes.Add(box, new List<Lens>());

                    var oldLens = boxes[box].Where(l => l.Label == lens.Label).FirstOrDefault();

                    if (oldLens != null) oldLens.FocalLenght = lens.FocalLenght;
                    else boxes[box].Add(lens);
                }
            }
            currentString = "";
        } 


        if(!partOne)
        {
            tot = boxes.Select(
                b => b.Value.Select(
                            (l, idx) => (b.Key + 1) * (idx + 1) * l.FocalLenght
                        ).Sum()
                    )
                    .Sum();
        }

        return tot;
    }

    private static int calculateHash(string input)
    {
        return input.Aggregate(0, (tot, c) => (tot + c) * 17 % 256);
    }

    class Lens
    {
        public string Label;
        public int FocalLenght;

        public Lens(string label, int focalLenght)
        {
            Label = label;
            FocalLenght = focalLenght;
        }
    }
}

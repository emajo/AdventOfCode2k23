class Day7
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day7.txt");

        var sortedHands = new List<Hand>();

        while (!sr.EndOfStream)
        {

            string line = sr.ReadLine();
            var splittedLine = line.Split(" ");

            var hand = new Hand(splittedLine[0], Convert.ToInt32(splittedLine[1]));

            if(sortedHands.Count() == 0)
            {
                sortedHands.Add(hand);
                continue;
            }

            for (int i = 0; i < sortedHands.Count(); i++)
            {
                if (hand.IsGreater(sortedHands[i], !partOne))
                {
                    sortedHands.Insert(i, hand);
                    break;
                }
                if(i == sortedHands.Count() - 1)
                {
                    sortedHands.Add(hand);
                    break;
                }
            }
        
        }

        sr.Close();

        int tot = 0;

        // tot = sortedHands.Select((sh, idx) => sh.Value * (sortedHands.Count() - idx)).Sum();

        for(int i = 0; i < sortedHands.Count(); i++)
        {
            tot += sortedHands[i].Value * (sortedHands.Count() - i);
        }

        return tot;
    }

    class Hand
    {
        public string Sequence;
        public int Value;

        public Dictionary<char, int> Cards;

        public Hand(string sequence, int value)
        {
            Sequence = sequence;
            Value = value;
            Cards = new Dictionary<char, int>();
            foreach (char c in sequence)
            {
                if (Cards.ContainsKey(c))
                {
                    Cards[c]++;
                } else
                {
                    Cards.Add(c, 1);
                }
            }
        }

        public int CalculateCardValue()
        {
            int tot = 0;
            foreach (var card in Cards)
            {
                tot += (int)Math.Pow(card.Value, 2);
            }
            return tot;
        }

        public int CalculateSpecialJCardValue()
        {
            int tot = 0;
            if (Sequence == "JJJJJ") return 25;
            char maxKey = Cards.MaxBy(c => c.Key != 'J' ? c.Value : 0).Key;

            foreach (var card in Cards)
            {
                if (card.Key == 'J') continue;
                int cardVal = card.Value;
                if (card.Key == maxKey && Cards.ContainsKey('J'))
                {
                    cardVal += Cards['J'];
                }
                tot += (int)Math.Pow(cardVal, 2);
            }
            return tot;
        }

        public bool IsGreater(Hand comp, bool specialJ)
        {
            int val;
            int compVal;
            if(!specialJ)
            {
                val = CalculateCardValue();
                compVal = comp.CalculateCardValue();
            } else
            {
                cardRanks['J'] = 0;
                val = CalculateSpecialJCardValue();
                compVal = comp.CalculateSpecialJCardValue();
            }

            if (val == compVal) return SequenceIsGreater(comp.Sequence);

            return val > compVal;
        }

        private static Dictionary<char, int> cardRanks = new Dictionary<char, int>()
        {
            { 'A', 13 },
            { 'K', 12 },
            { 'Q', 11 },
            { 'J', 10 },
            { 'T', 9 },
            { '9', 8 },
            { '8', 7 },
            { '7', 6 },
            { '6', 5 },
            { '5', 4 },
            { '4', 3 },
            { '3', 2 },
            { '2', 1 },
        };


        private bool SequenceIsGreater(string comp)
        {
            for (int i = 0; i < Sequence.Count(); i++)
            {
                if (cardRanks[comp[i]] == cardRanks[Sequence[i]]) continue;
                return cardRanks[comp[i]] < cardRanks[Sequence[i]];
            }
            return true;
        }
    }
}

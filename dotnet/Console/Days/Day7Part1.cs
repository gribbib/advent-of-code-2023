
using System.Data;
using System.Globalization;

public class Day7Part1 : AbstractDays
{
    List<Hand> hands = new List<Hand>();
    public override void DoFinalThings()
    {
        hands.Sort();
        int i = 1;
        Result = hands.Select(h => h.Points * i++).Sum();
    }

    public override void DoLoopThings(string line)
    {
        var split = line.Trim().Split(' ');
        hands.Add(new Hand(split[0], split[1]));
    }
}

class Hand : IComparable
{
    private string _handValue;
    public string HandValue
    {
        get => _handValue; private set
        {
            _handValue = value;

            // Determine HandType
            var handValueDictionary = _handValue.GroupBy(v => v).ToDictionary(g => g.Key, g => g.Count());
            var dictionaryCount = handValueDictionary.Count();
            switch (dictionaryCount)
            {
                case 1:
                    HandType = HandType.FiveOfAKind;
                    break;
                case 2: // FourOfAKind or FullHouse
                    if (handValueDictionary.First().Value == 1 || handValueDictionary.First().Value == 4)
                    {

                        HandType = HandType.FourOfAKind;
                    }
                    else
                    {
                        HandType = HandType.FullHouse;
                    }
                    break;
                case 3: // ThreeOfAKind or TwoPair
                    if (handValueDictionary.Where(d => d.Value == 1).Count() == 2)
                    {
                        HandType = HandType.ThreeOfAKind;
                    }
                    else
                    {
                        HandType = HandType.TwoPair;
                    }
                    break;
                case 4:
                    HandType = HandType.OnePair;
                    break;
                case 5:
                    HandType = HandType.HighCard;
                    break;
                default: break;
            }
        }
    }

    public HandType HandType { get; private set; }
    public int Points { get; private set; }

    public Hand(string handValue, int points)
    {
        HandValue = handValue;
        Points = points;
    }

    public Hand(string handValue, string points) : this(handValue, int.Parse(points))
    {
    }

    public int[] GetComparableIntValuesFromHand()
    {
        return HandValue.Select(c =>
        {
            // Try parse for numeric values
            if (int.TryParse(c.ToString(), out int i))
            {
                return i;
            }

            switch (c)
            {
                case 'T': return 10;
                case 'J': return 11;
                case 'Q': return 12;
                case 'K': return 13;
                case 'A': return 14;
            }

            return 0;
        }).ToArray();
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is Hand otherHand)
        {
            var compareValue = HandType.CompareTo(otherHand.HandType);
            if (compareValue == 0)
            {
                // compare each position as the HandType is the same
                var thisHandValueInts = GetComparableIntValuesFromHand();
                var otherHandValueInts = otherHand.GetComparableIntValuesFromHand();
                for (int i = 0; i < HandValue.Length; i++)
                {
                    var comparePositionValue = thisHandValueInts[i].CompareTo(otherHandValueInts[i]);
                    if (comparePositionValue != 0)
                    {
                        return comparePositionValue;
                    }
                }
                return 0;
            }

            return compareValue;
        }
        else
            throw new ArgumentException("Object is not a Hand");
    }
}

enum HandType
{
    HighCard, //23456
    OnePair, //A23A4
    TwoPair, //23432
    ThreeOfAKind, //TTT98
    FullHouse, //23332
    FourOfAKind, //AA8AA
    FiveOfAKind, //AAAAA
}
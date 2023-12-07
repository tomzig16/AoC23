using System.Text.RegularExpressions;
using Utils;

namespace Day7
{
    class Program
    {
        enum HandType
        {
            HighCard = 1,
            OnePair,
            TwoPairs,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        class HandData
        {
            public int handBid = 0;
            public Dictionary<short, short> cardsInHand;
            public HandType thisHandType;
            private string originalString;
            
            public HandData(string handString, string bid)
            {
                originalString = handString;
                handBid = int.Parse(bid);
                cardsInHand = new Dictionary<short, short>();

                foreach (var card in handString)
                {
                    short cardValue = GetCardValueFromChar(card);

                    cardsInHand.TryAdd(cardValue, 0);
                    cardsInHand[cardValue]++;
                }
                
                GenerateHandType();
            }

            private short GetCardValueFromChar(char card)
            {
                short cardValue = 0;
                if (!short.TryParse(card.ToString(), out cardValue))
                {
                    switch(card)
                    {
                        case 'T':
                            cardValue = 10;
                            break;
                        case 'J':
                            cardValue = 11;
                            break;
                        case 'Q':
                            cardValue = 12;
                            break;
                        case 'K':
                            cardValue = 13;
                            break;
                        case 'A':
                            cardValue = 14;
                            break;
                    }
                }

                return cardValue;
            }
            
            private void GenerateHandType()
            {
                if (cardsInHand.Count == 1)
                {
                    thisHandType = HandType.FiveOfAKind;
                    return;
                }
                
                if (cardsInHand.Count == 2)
                {
                    if (cardsInHand.ContainsValue(4))
                    {
                        thisHandType = HandType.FourOfAKind;
                    }
                    else
                    {
                        thisHandType = HandType.FullHouse;
                    }

                    return;
                }
                
                if (cardsInHand.Count == 3)
                {
                    if (cardsInHand.ContainsValue(3))
                    {
                        thisHandType = HandType.ThreeOfAKind;
                    }
                    else
                    {
                        thisHandType = HandType.TwoPairs;
                    }

                    return;
                }
                
                if (cardsInHand.Count == 4)
                {
                    thisHandType = HandType.OnePair;
                    return;
                }

                thisHandType = HandType.HighCard;
                // return;
            }


            public int CompareTo(HandData hand2)
            {
                int comparisonResult = thisHandType.CompareTo(hand2.thisHandType);
                if (comparisonResult == 0)
                {
                    for (int i = 0; i < originalString.Length; i++)
                    {
                        comparisonResult = GetCardValueFromChar(originalString[i])
                            .CompareTo(GetCardValueFromChar(hand2.originalString[i]));
                        if (comparisonResult != 0)
                        {
                            return comparisonResult;
                        }
                    }

                    return 0;
                }

                return comparisonResult;

            }
        }
        
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(7);
            
            Console.WriteLine(Part1(inputs));
            Console.WriteLine(Part2(inputs));
            
        }

        private static long Part1(List<string> inputs)
        {
            long resultMult = 0;

            List<HandData> allHands = new List<HandData>(inputs.Count);
            
            foreach (var line in inputs)
            {
                string[] lineData = line.Split(' ');
                allHands.Add(new HandData(lineData[0], lineData[1]));
            }
            
            allHands.Sort((hand1, hand2) => hand1.CompareTo(hand2));

            for (int i = 0; i < allHands.Count; i++)
            {
                resultMult += (allHands[i].handBid * (i + 1));
            }
            
            return resultMult;
        }
        
        private static long Part2(List<string> inputs)
        {
            return 0;
        }
    }
}
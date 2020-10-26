using System;
using System.Collections.Generic;

namespace Yatzy
{
    public class Scorecard
    {
        public Dictionary<Category, int>Scores ;
        List<ICategory> categories;

        public Scorecard()
        {
            Scores = new Dictionary<Category, int>();
            foreach(Category cat in Enum.GetValues(typeof(Category)))
            {
                Scores.Add(cat, -1);
            }
            categories = new List<ICategory> { new Ones(), new Twos(), new Threes(), new Fours(), new Fives(), 
                new Sixes(), new Pairs(), new TwoPairs(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(),
                new SmallStraight(), new LargeStraight(), new Yatzee(), new Chance() };
        }

        public void AddScore(int[] roll, Category value)
        {
            var category = categories.Find(_ => _.Id == (int)value);
            var score = category.Score(roll);
            Scores.Remove(value);
            Scores.Add(value, score);
        }

         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class SelekcjaTurniejowa
    {
        public List<Individual> SelectDouble(List<Individual> individuals, int k, bool maximalization = true) 
        {
            Random random = new Random();

            List<Individual> winners = new List<Individual>();

            var mixedIndividuals = individuals.OrderBy(x => random.Next());

            for (var i = 0; i < mixedIndividuals.Count() / 3; i++)
            {
                var x = mixedIndividuals.Skip(i * 3).Take(3);
                winners.Add(maximalization ?  x.OrderByDescending(z => z.FunctionResult).First() : x.OrderBy(z => z.FunctionResult).First());
            }

            return winners;
        }

        public Individual SelectSingle(List<Individual> individuals, int k) 
        {
            var winners = SelectDouble(individuals, k);
            return winners.OrderByDescending(item => item.FunctionResult).First();
        }
    }
}

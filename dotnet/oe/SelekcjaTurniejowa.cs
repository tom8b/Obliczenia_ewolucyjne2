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
            var ordered = individuals.OrderByDescending(x => x.FunctionResult);

            Random random = new Random();

            List<Individual> winners = null;
            Individual winner = null;

            var mixedIndividuals = individuals.OrderBy(x => random.Next()).ToArray();

            for (var i = 0; i < (float)mixedIndividuals.Length / 3; i++)
            {
                var x = mixedIndividuals.Skip(i * 3).Take(3);
                winners.Append(maximalization ?  x.Max() : x.Min());
            }

            winner = winners.OrderByDescending(item => item.FunctionResult).First();

            return winners;
        }

        public Individual SelectSingle(List<Individual> individuals, int k) 
        {
            var winners = SelectDouble(individuals, k);
            return winners.OrderByDescending(item => item.FunctionResult).First();
        }
    }
}

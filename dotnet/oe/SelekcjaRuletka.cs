using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class SelekcjaRuletka
    {
        public List<Individual> Select(List<Individual> individuals, int amountOfSpins)
        {
            List<Individual> winners = new List<Individual>();

            for(int i=0;i<amountOfSpins;i++)
            {
                winners.Add(SingleSpin(individuals));
            }

            return winners;
        }

        private Individual SingleSpin(List<Individual> individuals)
        {
            double[] c;
            double total;
            Random random;

            random = new Random();
            total = 0;
            c = new double[individuals.Count + 1];
            c[0] = 0;
            for (int i = 0; i < individuals.Count; i++)
            {
                c[i + 1] = c[i] + individuals[i].FunctionResult;
                total += individuals[i].FunctionResult;
            }

            double r = random.NextDouble() * total;
            int a = 0;
            int b = c.Length - 1;
            while (b - a > 1)
            {
                int mid = (a + b) / 2;
                if (c[mid] > r) b = mid;
                else a = mid;
            }

            return individuals[a];
        }
    }
}

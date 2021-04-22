using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class IndividualGenerator
    {
        public List<Individual> GenerateList(int count, int a, int b)
        {
            var result = new List<Individual>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Individual(GetDouble(a, b), GetDouble(a, b)));
            }

            return result;
        }

        private double GetDouble(double maxValue, double minValue)
        {
            var random = new Random();
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}

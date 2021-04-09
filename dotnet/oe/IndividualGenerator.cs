using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class IndividualGenerator
    {
        public List<Individual> GenerateList(int count, int numberOfBits, int a, int b)
        {
            var result = new List<Individual>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Individual (CreateXFor(numberOfBits), CreateXFor(numberOfBits), a, b));
            }

            return result;
        }

        private int[] CreateXFor(int numberOfBits)
        {
            var array = new int[numberOfBits];

            for (int i = 0; i < numberOfBits; i++)
            {
                array[i] = GetRandomZeroOne();
            }

            return array;
        }

        private int GetRandomZeroOne()
        {
            Random random = new Random();
            var randomNumber = random.Next(0, 2);
            return randomNumber;
        }
    }
}

using System.Collections.Generic;

namespace ConsoleApp1
{
    public class KrzyzowanieJednorodne
    {
        public List<Individual> Krzyzuj(Individual firstIndividual, Individual secondIndividual, int a, int b)
        {
            var firstIndividualX1 = new int[firstIndividual.NumberOfBits];
            var firstIndividualX2 = new int[firstIndividual.NumberOfBits];

            var secondIndividualX1 = new int[firstIndividual.NumberOfBits];
            var secondIndividualX2 = new int[firstIndividual.NumberOfBits];

            for (int i = 0; i < firstIndividual.NumberOfBits; i++)
            {
                firstIndividualX1[i] = i % 2 == 0 ? firstIndividual.X1Binary[i] : secondIndividual.X1Binary[i];
                firstIndividualX2[i] = i % 2 == 0 ? firstIndividual.X2Binary[i] : secondIndividual.X2Binary[i];
            }

            for (int i = 0; i < secondIndividual.NumberOfBits; i++)
            {
                secondIndividualX1[i] = i % 2 == 0 ? secondIndividual.X1Binary[i] : firstIndividual.X1Binary[i];
                secondIndividualX2[i] = i % 2 == 0 ? secondIndividual.X2Binary[i] : firstIndividual.X2Binary[i];
            }

            return new List<Individual>
            {
                new Individual(firstIndividualX1, firstIndividualX2, a, b),
                new Individual(secondIndividualX1, secondIndividualX2, a, b)
            };
        }
    }
}

using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class KrzyzowanieJednopunktowe
    {
        public List<Individual> Krzyzuj(Individual firstIndividual, Individual secondIndividual, int a, int b)
        {
            int[] firstIndividualResultX1 = new int[firstIndividual.NumberOfBits];
            int[] firstIndividualResultX2 = new int[firstIndividual.NumberOfBits];

            int[] secondIndividualResultX1 = new int[firstIndividual.NumberOfBits];
            int[] secondIndividualResultX2 = new int[firstIndividual.NumberOfBits];

            var punktKrzyzowania = WybierzPunktKrzyzowania(firstIndividual.NumberOfBits);

            for (int i = 0; i < firstIndividual.NumberOfBits; i++)
            {
                firstIndividualResultX1[i] = i < punktKrzyzowania ? firstIndividual.X1Binary[i] : secondIndividual.X1Binary[i];
                firstIndividualResultX2[i] = i < punktKrzyzowania ? firstIndividual.X2Binary[i] : secondIndividual.X2Binary[i];

                secondIndividualResultX1[i] = i < punktKrzyzowania ? secondIndividual.X1Binary[i] : firstIndividual.X1Binary[i];
                secondIndividualResultX2[i] = i < punktKrzyzowania ? secondIndividual.X2Binary[i] : firstIndividual.X2Binary[i];
            }

            List<Individual> individuals = new List<Individual>();
            individuals.Add(new Individual(firstIndividualResultX1, firstIndividualResultX2, a, b));
            individuals.Add(new Individual(secondIndividualResultX2, secondIndividualResultX2, a, b));

            return individuals;
        }

        private int WybierzPunktKrzyzowania(int numberOfBits)
        {
            Random random = new Random();
            return random.Next(1, numberOfBits-1);
        }
    }
}

using System;

namespace ConsoleApp1
{
    public class MutacjaDwupunktowa
    {
        public Individual Mutuj(Individual individual)
        {
            var punktMutacji = WybierzPunktMutacji(individual.NumberOfBits);

            individual.X1Binary[punktMutacji] = individual.X1Binary[punktMutacji] == 0 ? 1 : 0;
            individual.X2Binary[punktMutacji] = individual.X2Binary[punktMutacji] == 0 ? 1 : 0;

            var drugiPunktMutacji = WybierzPunktMutacji(individual.NumberOfBits, punktMutacji);

            individual.X1Binary[drugiPunktMutacji] = individual.X1Binary[drugiPunktMutacji] == 0 ? 1 : 0;
            individual.X2Binary[drugiPunktMutacji] = individual.X2Binary[drugiPunktMutacji] == 0 ? 1 : 0;

            return individual;
        }

        private int WybierzPunktMutacji(int numberOfBits, int differentThan = -1)
        {
            Random random = new Random();
            var randomNumber = differentThan; 
            while (randomNumber == differentThan)
            {
                randomNumber = random.Next(1, numberOfBits - 1);
            }

            return randomNumber;
        }
    }
}
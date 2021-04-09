namespace ConsoleApp1
{
    public class MutacjaBrzegowa
    {
        public Individual Mutuj(Individual individual)
        {
            individual.X1Binary[individual.NumberOfBits - 1] =
                individual.X1Binary[individual.NumberOfBits - 1] == 0 ? 1 : 0;

            individual.X2Binary[individual.NumberOfBits - 1] =
                individual.X2Binary[individual.NumberOfBits - 1] == 0 ? 1 : 0;

            return individual;
        }
    }
}

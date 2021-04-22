namespace ConsoleApp1
{
    public static class BoothFunction
    {
        // BOOTH FUNCTION
        public static double CalculateFor(Individual individual)
        {
            return (individual.X1 + (double)2 * individual.X2 - (double)7) * (individual.X1 + (double)2 * individual.X2 - (double)7) + ((double)2 * individual.X1 + individual.X2 - (double)5) * ((double)2 * individual.X1 + individual.X2 - (double)5);
        }
    }
}

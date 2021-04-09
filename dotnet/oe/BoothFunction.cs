namespace ConsoleApp1
{
    public static class BoothFunction
    {
        // BOOTH FUNCTION
        public static double CalculateFor(Individual individual)
        {
            return (individual.GetX1Dec() + (double)2 * individual.GetX2Dec() - (double)7) * (individual.GetX1Dec() + (double)2 * individual.GetX2Dec() - (double)7) + ((double)2 * individual.GetX1Dec() + individual.GetX2Dec() - (double)5) * ((double)2 * individual.GetX1Dec() + individual.GetX2Dec() - (double)5);
        }
    }
}

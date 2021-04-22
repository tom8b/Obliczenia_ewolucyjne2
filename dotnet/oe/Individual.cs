namespace ConsoleApp1
{
    public class Individual
    {
        private readonly double _x1;
        private readonly double _x2;

        public Individual(double x1, double x2)
        {
            _x1 = x1;
            _x2 = x2;
        }

        public double X1 => _x1;
        public double X2 => _x2;

        public double FunctionResult => BoothFunction.CalculateFor(this);
    }
}

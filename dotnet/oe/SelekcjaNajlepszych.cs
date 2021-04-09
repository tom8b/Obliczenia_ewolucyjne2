using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class SelekcjaNajlepszych
    {
        public List<Individual> Select(List<Individual> individuals, double percent, bool maximalization = true) //procenty = od 0 do 1
        {
            var ordered = maximalization ? individuals.OrderByDescending(x => x.FunctionResult) : individuals.OrderBy(x => x.FunctionResult);
            var numberOfChoosen = (int)(percent * individuals.Count());

            return ordered.Take(numberOfChoosen).ToList();
        }
    }
}

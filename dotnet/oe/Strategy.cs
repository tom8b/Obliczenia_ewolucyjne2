using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Strategy
    {
        private readonly IndividualGenerator _individualGenerator;

        private readonly SelekcjaNajlepszych _selekcjaNajlepszych;
        private readonly SelekcjaTurniejowa _selekcjaTurniejowa;
        private readonly SelekcjaRuletka _selekcjaRuletka;

        private readonly KrzyzowanieJednopunktowe _krzyzowanieJednopunktowe;
        private readonly KrzyzowanieDwupunktowe _krzyzowanieDwupunktowe;
        private readonly KrzyzowanieJednorodne _krzyzowanieJednorodne;

        private readonly MutacjaJednopunktowa _mutacjaJednopunktowa;
        private readonly MutacjaDwupunktowa _mutacjaDwupunktowa;
        private readonly MutacjaBrzegowa _mutacjaBrzegowa;
        private readonly Inwersja _inwersja;

        public Strategy(IndividualGenerator individualGenerator, SelekcjaNajlepszych selekcjaNajlepszych, SelekcjaTurniejowa selekcjaTurniejowa,SelekcjaRuletka selekcjaRuletka,
            KrzyzowanieJednopunktowe krzyzowanieJednopunktowe, KrzyzowanieDwupunktowe krzyzowanieDwupunktowe, KrzyzowanieJednorodne krzyzowanieJednorodne,
            MutacjaJednopunktowa mutacjaJednopunktowa, MutacjaDwupunktowa mutacjaDwupunktowa, MutacjaBrzegowa mutacjaBrzegowa, Inwersja inwersja)
        {
            _individualGenerator = individualGenerator;
            _selekcjaNajlepszych = selekcjaNajlepszych;
            _selekcjaRuletka = selekcjaRuletka;
            _selekcjaTurniejowa = selekcjaTurniejowa;           
            _krzyzowanieJednopunktowe = krzyzowanieJednopunktowe;
            _krzyzowanieDwupunktowe = krzyzowanieDwupunktowe;
            _krzyzowanieJednorodne = krzyzowanieJednorodne;
            _mutacjaJednopunktowa = mutacjaJednopunktowa;
            _mutacjaDwupunktowa = mutacjaDwupunktowa;
            _mutacjaBrzegowa = mutacjaBrzegowa;
            _inwersja = inwersja;
        }

        public List<Individual> Execute(int a, int b, int populationAmount, int numberOfBits, int epochsAmount, double bestAndTournamentChomosomeAmount, double eliteStrategyAmount, double crossProbability, double mutationProbability, double inversionProbability, SelectionMethod selectionMethod, CrossMethod crossMethod, MutationMethod mutationMethod, bool maximization)
        {
            //Poczatkowa populacja
            var population = _individualGenerator.GenerateList(populationAmount, numberOfBits, a, b);

            for (int i = 0; i < epochsAmount; i++)
            {
                //Selekcja
                var afterSelection = Wyselekcjuj(selectionMethod, population, bestAndTournamentChomosomeAmount, maximization);

                var newPopulation = new List<Individual>();

                // strategia elitarna - wez X procent najlepszych do nowej populacji
                newPopulation.AddRange(_selekcjaNajlepszych.Select(afterSelection, eliteStrategyAmount, maximization));

                //Mutacja, krzyzowanie i inwersja
                while (newPopulation.Count < population.Count)
                {
                    if (ShouldPerformAction(crossProbability) && newPopulation.Count + 1 < population.Count)
                    {
                        newPopulation.AddRange(Krzyzuj(crossMethod, afterSelection, a, b));
                    }

                    if (ShouldPerformAction(mutationProbability) && newPopulation.Count < population.Count)
                    {
                        newPopulation.Add(Mutuj(mutationMethod, afterSelection, a, b));
                    }

                    if (ShouldPerformAction(inversionProbability) && newPopulation.Count < population.Count)
                    {
                        newPopulation.Add(Inwersuj(afterSelection, a, b));
                    }
                }

                population = newPopulation;
            }

            return population;
        }

        private Individual Inwersuj(List<Individual> population, int a, int b)
        {
            var randomIndividual = GetRandomIndividual(population);
            return _inwersja.Inwersuj(randomIndividual, a, b);
        }

        private Individual Mutuj(MutationMethod mutationMethod, List<Individual> population, int a, int b)
        {
            var randomIndividual = GetRandomIndividual(population);

            switch (mutationMethod)
            {
                case MutationMethod.ONE_POINT:
                    return _mutacjaJednopunktowa.Mutuj(randomIndividual);
                case MutationMethod.TWO_POINT:
                    return _mutacjaDwupunktowa.Mutuj(randomIndividual);
                case MutationMethod.BRZEGOWA:
                    return _mutacjaBrzegowa.Mutuj(randomIndividual);
                default:
                    throw new NotImplementedException();
            }
        }

        private bool ShouldPerformAction(double probability)
        {
            Random random = new Random();
            var n = random.NextDouble();
            return n < probability;
        }

        private Individual GetRandomIndividual(List<Individual> population)
        {
            Random random = new Random();
            var firstIndex = random.Next(0, population.Count);

            return population[firstIndex];
        }

        private List<Individual> GetTwoRandomIndividuals(List<Individual> population)
        {
            Random random = new Random();
            var firstIndex = random.Next(0, population.Count);
            int secondIndex = firstIndex;
            while (firstIndex == secondIndex)
            {
                secondIndex = random.Next(0, population.Count);
            }

            return new List<Individual>
            {
                population[firstIndex],
                population[secondIndex]
            };
        }

        private List<Individual> Krzyzuj(CrossMethod crossMethod, List<Individual> population, int a, int b)
        {
            var twoRandomIndividuals = GetTwoRandomIndividuals(population);

            switch (crossMethod)
            {
                case CrossMethod.ONE_POINT:
                    return _krzyzowanieJednopunktowe.Krzyzuj(twoRandomIndividuals[0], twoRandomIndividuals[1], a, b);
                case CrossMethod.TWO_POINT:
                    return _krzyzowanieDwupunktowe.Krzyzuj(twoRandomIndividuals[0], twoRandomIndividuals[1], a, b);
                case CrossMethod.JEDNORODNE:
                    return _krzyzowanieJednorodne.Krzyzuj(twoRandomIndividuals[0], twoRandomIndividuals[1], a, b);
                default:
                    throw new NotImplementedException();
            }
        }

        private List<Individual> Wyselekcjuj(SelectionMethod selectionMethod, List<Individual> population, double bestChromosomeAmount, bool maximization)
        {
            switch (selectionMethod)
            {
                case SelectionMethod.BEST:
                    return _selekcjaNajlepszych.Select(population, bestChromosomeAmount, maximization);
                case SelectionMethod.Kolo_Ruletki:
                    return _selekcjaRuletka.Select(population, (int)bestChromosomeAmount);
                case SelectionMethod.Turniejowa:
                    return _selekcjaTurniejowa.SelectDouble(population, (int)bestChromosomeAmount, maximization);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum SelectionMethod
    {
        BEST,
        Kolo_Ruletki,
        Turniejowa
    }

    public enum CrossMethod
    {
        ONE_POINT,
        TWO_POINT,
        THREE_POINT,
        JEDNORODNE
    }

    public enum MutationMethod
    {
        ONE_POINT,
        TWO_POINT,
        BRZEGOWA,
    }
}

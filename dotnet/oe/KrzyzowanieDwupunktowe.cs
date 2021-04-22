using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    //public class KrzyzowanieDwupunktowe
    //{
    //    public List<Individual> Krzyzuj(Individual firstIndividual, Individual secondIndividual, int a, int b)
    //    {
    //        int[] firstIndividualResultX1 = new int[firstIndividual.NumberOfBits];
    //        int[] firstIndividualResultX2 = new int[firstIndividual.NumberOfBits];

    //        int[] secondIndividualResultX1 = new int[firstIndividual.NumberOfBits];
    //        int[] secondIndividualResultX2 = new int[firstIndividual.NumberOfBits];

    //        var punktyPrzeciecia = WybierzPunktyKrzyzowania(firstIndividual.NumberOfBits);

    //        for (int i = 0; i < firstIndividual.NumberOfBits; i++)
    //        {
    //            if(i > punktyPrzeciecia[0] && i < punktyPrzeciecia[1])
    //            {
    //                firstIndividualResultX1[i] = secondIndividual.X1Binary[i];
    //                firstIndividualResultX2[i] = secondIndividual.X2Binary[i];

    //                secondIndividualResultX1[i] = firstIndividual.X1Binary[i];
    //                secondIndividualResultX2[i] = firstIndividual.X2Binary[i];
    //            }
    //            else
    //            {
    //                firstIndividualResultX1[i] = firstIndividual.X1Binary[i];
    //                firstIndividualResultX2[i] = firstIndividual.X2Binary[i];

    //                secondIndividualResultX1[i] = secondIndividual.X1Binary[i];
    //                secondIndividualResultX2[i] = secondIndividual.X2Binary[i];
    //            }
    //        }

    //        List<Individual> individuals = new List<Individual>();
    //        individuals.Add(new Individual(firstIndividualResultX1, firstIndividualResultX2, a, b));
    //        individuals.Add(new Individual(secondIndividualResultX2, secondIndividualResultX2, a, b));

    //        return individuals;
    //    }

    //    private int[] WybierzPunktyKrzyzowania(int numberOfBits)
    //    {
    //        Random random = new Random();
    //        var firstValue = random.Next(1, numberOfBits - 1);
    //        var secondValue = random.Next(firstValue, numberOfBits - 1);
    //        return new int[] { firstValue, secondValue };
    //    }
    //}
}

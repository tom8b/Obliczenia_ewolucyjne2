using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class DataSaver
    {
        public string fileName { get; set; }

        public bool saveIndividualsFromEpoche(int epoche, List<Individual> individuals)
        {
            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine($"#{epoche}#");
                foreach (var individual in individuals)
                {
                    w.WriteLine($"X1:{individual.GetX1Dec()} X2:{individual.GetX2Dec()} Y:{individual.FunctionResult}\n");
                }

                return true;
            }
        }

        public void createFile()
        {
            fileName = Directory.GetCurrentDirectory() + "\\results\\results" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            using (FileStream fs = File.Create(fileName))
            {
            }
        }
    }
}

using OxyPlot;
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
                    w.WriteLine($"X1:{individual.X1} X2:{individual.X2} Y:{individual.FunctionResult}\n");
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

        public void saveChart(List<double> individuals)
        {
            var line1 = new OxyPlot.Series.LineSeries
            {
                Title = $"Y",
                Color = OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = MarkerType.Circle
            };

            for (int i = 0; i < individuals.Count; i++)
            {
                line1.Points.Add(new DataPoint(i + 1, individuals[i]));
            }

            var model = new PlotModel
            {
                Title = $"wykres wartosci funkcji w zaleznosci od epok",
            };
            model.Series.Add(line1);
            var chartName = Directory.GetCurrentDirectory() + "\\results\\chart" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".pdf";

            using (var stream = File.Create(chartName))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(model, stream);
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CrimeAnalyzer

{

    public class CrimeStatistics

    {

        public int robbery;
        public int assault;
        public int property;
        public int burglary;
        public int theft;
        public int vehicle;
        public int year;
        public int murder;
        public int rape;
        public int population;
        public int violentcrime;



        public CrimeStatistics(int population, int year, int rape, int murder, int violentcrime, int robbery, int assault, int theft, int burglary, int property, int vehicle)

        {

            this.robbery = robbery;
            this.assault = assault;
            this.property = property;
            this.burglary = burglary;
            this.theft = theft;
            this.vehicle = vehicle;
            this.year = year;
            this.population = population;
            this.violentcrime = violentcrime;
            this.murder = murder;
            this.rape = rape;


        }

    }


    class Program

    {

        static void Main(string[] args)

        {
            String file1;
            String wFile;
            List<CrimeStatistics> list = new List<CrimeStatistics>();
            int count = 0;

            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect arguments. Format Should Be \n dotnet CrimeAnalyzer.dll <csv_file_path> <report_file_path>  \n");
                Environment.Exit(-1);
            }

            file1 = args[0];

            if (File.Exists(file1) == false)
            {
                Console.WriteLine("File Does Not Exist. ");
                Environment.Exit(-1);
            }

            using (var reader = new StreamReader(file1))
            {


                string header = reader.ReadLine();
                var hValues = header.Split(',');

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    int year = Convert.ToInt32(values[0]);
                    int population = Convert.ToInt32(values[1]);
                    int violentcrime = Convert.ToInt32(values[2]);
                    int murder = Convert.ToInt32(values[3]);
                    int rape = Convert.ToInt32(values[4]);
                    int robbery = Convert.ToInt32(values[5]);
                    int assault = Convert.ToInt32(values[6]);
                    int property = Convert.ToInt32(values[7]);
                    int burglary = Convert.ToInt32(values[8]);
                    int theft = Convert.ToInt32(values[9]);
                    int vehicle = Convert.ToInt32(values[10]);


                    list.Add(new crimeStatistics(year, population, violentcrime, rape, murder, robbery, assault, property, burglary, theft, vehicle));
                }



            }



            string report = "";

            var years = from CrimeStatistics in list select CrimeStatistics.year;
            foreach (var x in years)
            {
                count++;
            }

            var q3Murders = from CrimeStatistics in list where CrimeStatistics.murder < 15000 select CrimeStatistics.year;

            var q4Robberies = from CrimeStatistics in list where CrimeStatistics.robbery > 500000 select new { CrimeStatistics.year, CrimeStatistics.robbery };

            var q5Violence = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.violentcrime;
            var q5Capita = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.population;
            double v = 0;
            double c = 0;
            foreach (var x in q5Violence)
            {
                v = (double)x;
            }

            foreach (var x in q5Capita)
            {
                c = (double)x;
            }


            double q5Answer = v / c;

            var q6 = from CrimeStatistics in list select CrimeStatistics.murder;
            double q6Murder = 0;
            foreach (var x in q6)
            {
                q6Murder += x;
            }

            double q6Answer = q6Murder / count;

            var q7 = from CrimeStatistics in list where CrimeStatistics.year >= 1994 && CrimeStatistics.year <= 1997 select CrimeStatistics.murder;
            double q7Murder = 0;
            int q7Count = 0;
            foreach (var x in q7)
            {
                q7Murder += x;
                q7Count++;
            }

            double q7Answer = q7Murder / q7Count;

            var q8 = from CrimeStatistics in list where CrimeStatistics.year >= 2010 && CrimeStatistics.year <= 2013 select CrimeStatistics.murder;
            double q8Murder = 0;
            int q8Count = 0;
            foreach (var x in q8)
            {
                q8Murder += x;
                q8Count++;
            }

            double q8Answer = q8Murder / q8Count;


            var q9 = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;

            int q9Answer = q9.Min();

            var q10 = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;

            int q10Answer = q10.Max();

            var q11 = from CrimeStatistics in list select new { CrimeStatistics.year, CrimeStatistics.vehicle };
            int q11Answer = 0;
            int temp = 0;

            foreach (var x in q11)
            {

                if (x.vehicle > temp)
                {
                    q11Answer = x.year;
                    temp = x.vehicle;
                }
            }






            report += "The Range Of Years Include " + years.Min() + " - " + years.Max() + " (" + count + " years) \n";


            report += "Years Murders Per Year < 15000: ";
            foreach (var x in q3Murders)
            {
                report += x + " ";
            }
            report += "\n";

            report += "Robberies Per Year > 500000: ";
            foreach (var x in q4Robberies)
            {
                report += string.Format("{0} = {1}, ", x.year, x.robbery);
            }

            report += "\n";

            report += "Violent Crime Per Capita Rate (2010): " + q5Answer + "\n";

            report += "Average Murder Per Year (Across All Years): " + q6Answer + "\n";

            report += "Average Murder Per Year (1994 To 1997): " + q7Answer + "\n";

            report += "Average Murder Per Year (2010 To 2013): " + q8Answer + "\n";

            report += "Minimum Thefts Per Year (1999 To 2004): " + q9Answer + "\n";

            report += "Maximum Thefts Per Year (1999 To 2004): " + q10Answer + "\n";

            report += "Year Of Highest Number Of Motor Vehicle Thefts: " + q11Answer + "\n";





            wFile = "Output.txt";

            StreamWriter sw = new StreamWriter(wFile);

            try
            {

                sw.WriteLine(report);


            }
            catch (Exception x)
            {
                Console.WriteLine("Exception: " + x.Message);
            }
            finally
            {
                Console.WriteLine("Executing.");

                sw.Close();

            }
        }




        
   

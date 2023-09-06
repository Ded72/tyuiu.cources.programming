using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyuiu.cources.programming
{
    public class CsvController
    {
        public string[,] ReadLinksFromCsv()
        {
            string pathCsvFile = "c:\\temp\\links.csv";
            string file = System.IO.File.ReadAllText(pathCsvFile);
            file = file.Replace('\n', '\r');
            string[] lines = file.Split(new char[] { '\r' },
            StringSplitOptions.RemoveEmptyEntries);
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;
            string[,] values = new string[num_rows, 1];
            for (int r = 1; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                values[r, 0] = line_r[num_cols - 1];
                //Console.WriteLine(values[r, 0]);    

            }
            return values;
        }
    }
}

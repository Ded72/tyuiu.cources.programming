using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.Interfaces;

namespace tyuiu.cources.programming
{
    public class FileOutputController : IOutputController
    {
        private readonly string filename;

        public FileOutputController(string filename)
        {
            this.filename = filename;
        }

        public void WriteLine(string line)
        {
            using(var f = new StreamWriter(filename, true))
            {
                f.WriteLine(line);
            }
        }
    }
}

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
            using (var f = new StreamWriter(filename, true))
            {
                f.WriteLine(line);
            }
        }
        public void WriteValue(string message, object? value)
        {
            using (var f = new StreamWriter(filename, true))
            {
                var buffer = message;
                if (value!.GetType().IsArray)
                {
                    foreach (var item in (int[])value!)
                    {
                        buffer += $"{item} ";
                    }
                }
                else if(value!.GetType() == typeof(string))
                {
                    buffer += $"'{value}'";
                }
                else
                {
                    buffer += $"{value}";
                }
                f.WriteLine(buffer);
            }
        }
    }
}

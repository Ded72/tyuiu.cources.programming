using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyuiu.cources.programming.Interfaces
{
    public interface IOutputController
    {
        void WriteLine(string line);
        void WriteValue(string message, object? value);
    }
}

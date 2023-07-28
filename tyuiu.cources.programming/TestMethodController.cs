using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyuiu.cources.programming
{
    public class TestMethodController
    {
        private readonly AssemblyController assemblyController;
        private readonly DataController dataController;

        public TestMethodController(
            AssemblyController assemblyController, 
            DataController dataController) 
        { 
            this.assemblyController = assemblyController;
            this.dataController = dataController;
        }
        public void Test<T>() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;

namespace tyuiu.cources.programming
{
    public class TestingDataController
    {
        private Dictionary<Type, (object result, object[] args)> testData = new()
        {

            { typeof(ISprint1Task0V1), (-6.0, new object[] {})},
            { typeof(ISprint1Task0V2), (-6.0, new object[] {})},
            { typeof(ISprint1Task0V3), (-6.0, new object[] {})},
            { typeof(ISprint1Task0V4), (-6.0, new object[] {})},
            { typeof(ISprint1Task1V0), (2.0, new object[] {1, 9})},
            { typeof(ISprint1Task2V0), (2, new object[] {2})},
            { typeof(ISprint1Task3V0), (2.0, new object[] {1, 1})},
            { typeof(ISprint1Task4V0), (2.0, new object[] {1 , 1 })},
            { typeof(ISprint1Task6V0), ("", new object[] {"" })},
            { typeof(ISprint1Task7V0), (1, new object[] {2, 3, 1 })},
        };
        public (object result, object[] args) GetData(Type type)
        {
            return testData[type];
        }

    }
}

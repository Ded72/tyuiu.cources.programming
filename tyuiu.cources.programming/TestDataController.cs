using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming
{
    public class TestDataController
    {
        private Dictionary<Type, (object result, object[] args)> testData = new() 
        {
            { typeof(ISprint0Task0V0), ("54321", new object[] { "12345" })},
            { typeof(ISprint0Task0V1), (90, new object[] { 10 })},
            { typeof(ISprint0Task0V2), (20, new object[] { 10, 2 })},
            { typeof(ISprint1Task0V0), (
                new int[] { 1, 2, 3 },
                new object[] { new int[] { 2, 3, 1 }
            })}        
        };
        public (object result, object[] args) GetData<T>()
        {
            return testData[typeof(T)];
        }
    }
}

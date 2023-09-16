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

            //Task0

            { typeof(ISprint1Task0V1), (-6.0, new object[] {})},
            { typeof(ISprint1Task0V2), (1.0, new object[] {})},
            { typeof(ISprint1Task0V3), (1.0, new object[] {})},
            { typeof(ISprint1Task0V4), (6.0, new object[] {})},
            { typeof(ISprint1Task0V5), (12.0, new object[] {})},
            { typeof(ISprint1Task0V6), (15.0, new object[] {})},
            { typeof(ISprint1Task0V7), (5.0, new object[] {})},
            { typeof(ISprint1Task0V8), (1.875, new object[] {})},
            { typeof(ISprint1Task0V9), (3.0, new object[] {})},
            { typeof(ISprint1Task0V10), (-10.5, new object[] {})},
            { typeof(ISprint1Task0V11), (7.0, new object[] {})},
            { typeof(ISprint1Task0V12), (6.0, new object[] {})},
            { typeof(ISprint1Task0V13), (1.0, new object[] {})},
            { typeof(ISprint1Task0V14), (25.0, new object[] {})},
            { typeof(ISprint1Task0V15), (24.0, new object[] {})},
            { typeof(ISprint1Task0V16), (-7.0, new object[] {})},
            { typeof(ISprint1Task0V17), (5.0, new object[] {})},
            { typeof(ISprint1Task0V18), (3.0, new object[] {})},
            { typeof(ISprint1Task0V19), (10.0, new object[] {})},
            { typeof(ISprint1Task0V20), (13.0, new object[] {})},
            { typeof(ISprint1Task0V21), (17.0, new object[] {})},
            { typeof(ISprint1Task0V22), (10.0, new object[] {})},
            { typeof(ISprint1Task0V23), (13.0, new object[] {})},
            { typeof(ISprint1Task0V24), (2.0, new object[] {})},
            { typeof(ISprint1Task0V25), (3.0, new object[] {})},
            { typeof(ISprint1Task0V26), (7.0, new object[] {})},
            { typeof(ISprint1Task0V27), (22.0, new object[] {})},
            { typeof(ISprint1Task0V28), (2.0, new object[] {})},
            { typeof(ISprint1Task0V29), (32.0, new object[] {})},
            { typeof(ISprint1Task0V30), (96.0, new object[] {})},

            //Task1

            { typeof(ISprint1Task1V0), (2.0, new object[] {1, 9})},
            { typeof(ISprint1Task1V1), (7.5, new object[] {9, 2, 1})},
            { typeof(ISprint1Task1V2), (0.5, new object[] {5, 1})},
            { typeof(ISprint1Task1V3), (3.0, new object[] {2, 2})},
            { typeof(ISprint1Task1V4), (4.0, new object[] {2, 2})},
            { typeof(ISprint1Task1V5), (8.0, new object[] {3, 3})},
            { typeof(ISprint1Task1V6), (2.0, new object[] {5, 1})},
            { typeof(ISprint1Task1V7), (2.5, new object[] {2, 2})},
            { typeof(ISprint1Task1V8), (3.14, new object[] {2, 2})},
            { typeof(ISprint1Task1V9), (0.875, new object[] {2, 2})},
            { typeof(ISprint1Task1V10), (1.0, new object[] {1, 1})},
            { typeof(ISprint1Task1V11), (5.0, new object[] {6, 1})},
            { typeof(ISprint1Task1V12), (1.0, new object[] {3, 3})},
            { typeof(ISprint1Task1V13), (2.0, new object[] {10})},
            { typeof(ISprint1Task1V14), (3.25, new object[] {1, 3, 1})},
            { typeof(ISprint1Task1V15), (2.0, new object[] {5})},
            

            //Task2

            { typeof(ISprint1Task2V0), (2.0, new object[] {2})},
            { typeof(ISprint1Task2V1), (1000, new object[] {1609})},
            { typeof(ISprint1Task2V2), (3.142, new object[] {180})},
            { typeof(ISprint1Task2V3), (360, new object[] {6})},
            { typeof(ISprint1Task2V4), (36.0, new object[] {6})},
            { typeof(ISprint1Task2V5), (216, new object[] {6})},
            { typeof(ISprint1Task2V6), (0.006, new object[] {6})},
            { typeof(ISprint1Task2V7), (113.097, new object[] {6})},
            { typeof(ISprint1Task2V8), (18, new object[] {6, 3})},
            { typeof(ISprint1Task2V9), (678.584, new object[] {6})},
            { typeof(ISprint1Task2V10), (236.85, new object[] {6})},
            { typeof(ISprint1Task2V11), (21780, new object[] {6, 3})},
            { typeof(ISprint1Task2V12), (36, new object[] {6, 2, 3})},
            { typeof(ISprint1Task2V13), (9.656, new object[] {6})},
            { typeof(ISprint1Task2V14), (282.85, new object[] {556})},
            { typeof(ISprint1Task2V15), (216, new object[] {6})},
           

            //Task3

            { typeof(ISprint1Task3V0), (2.0, new object[] {1, 1})},
            { typeof(ISprint1Task3V1), (6.28, new object[] {1, 2})},
            { typeof(ISprint1Task3V2), (11.0, new object[] {1, 5, 3, 2})},
            { typeof(ISprint1Task3V3), (3.0, new object[] {1, 1, 3})},
            { typeof(ISprint1Task3V4), (4.0, new object[] {1, 1, 2})},
            { typeof(ISprint1Task3V5), (1.0, new object[] {1, 1})},
            { typeof(ISprint1Task3V6), (0.1, new object[] {1, 1, 5})},
            { typeof(ISprint1Task3V7), (5.334, new object[] {5})},

            //Task4

            { typeof(ISprint1Task4V0), (2.000, new object[] {1 , 1 })},
            { typeof(ISprint1Task4V1), (1.000, new object[] {-1 })},
            { typeof(ISprint1Task4V2), (0.500, new object[] {2 , 1 })},
            { typeof(ISprint1Task4V3), (0.750, new object[] {3 , 1 })},
            { typeof(ISprint1Task4V4), (0.750, new object[] {2 , 1 })},
            { typeof(ISprint1Task4V5), (0.250, new object[] {2 , 4 })},
            { typeof(ISprint1Task4V6), (0.050, new object[] {360 , 30 })},
            { typeof(ISprint1Task4V7), (0.187, new object[] {2 , 2 })},
            { typeof(ISprint1Task4V8), (-0.6, new object[] {-2 , 7 })},
            { typeof(ISprint1Task4V9), (0.039, new object[] {120 , 30 })},
            { typeof(ISprint1Task4V10), (0.512, new object[] {60 })},
            { typeof(ISprint1Task4V11), (0.009, new object[] {5 , 5 })},
            { typeof(ISprint1Task4V12), (0.400, new object[] {0.5 , 44 })},
            { typeof(ISprint1Task4V13), (-0.045, new object[] {1 , 1 })},
            { typeof(ISprint1Task4V14), (0.007, new object[] {5, 5 })},
            { typeof(ISprint1Task4V15), (1969799074.119, new object[] {5 , 5 })},

            //Task5

            { typeof(ISprint1Task5V0), (2.000, new object[] {1 , 1 })},
            { typeof(ISprint1Task5V1), (4.0, new object[] {5 , 3, 2, 1 })},
            { typeof(ISprint1Task5V2), (-15.0, new object[] {5 })},
            { typeof(ISprint1Task5V3), (5.0, new object[] {230598 })},

            //Task6

            { typeof(ISprint1Task6V0), ("", new object[] {"" })},
            { typeof(ISprint1Task6V1), ("1", new object[] {"49" })},
            { typeof(ISprint1Task6V2), (true, new object[] {"Hello world" })},
            { typeof(ISprint1Task6V3), ("od", new object[] {"Hello world" })},
            { typeof(ISprint1Task6V4), ("желанный медленный", new object[] {"желанный юный медленный" })},
            { typeof(ISprint1Task6V5), ("шалаш", new object[] {"хороший шалаш" })},
            { typeof(ISprint1Task6V6), ("ривет ир", new object[] {"привет мир" })},
            

            //Task7

            { typeof(ISprint1Task7V0), (1, new object[] {2, 3, 1 })},
            { typeof(ISprint1Task7V1), (-28.579, new object[] {2, 4, 5 })},
            { typeof(ISprint1Task7V2), (-5.103, new object[] {2, 4})},
            { typeof(ISprint1Task7V3), (0.896, new object[] {2, 4 })},
            { typeof(ISprint1Task7V4), (0.236, new object[] {2, 4 })},
            { typeof(ISprint1Task7V5), (-0.544, new object[] {2, 4 })},
            { typeof(ISprint1Task7V6), (-188.000, new object[] {2, 4 })},
            { typeof(ISprint1Task7V7), (-6.641, new object[] {2, 4 })},
            { typeof(ISprint1Task7V8), (-9.676, new object[] {2, 4 })},
            { typeof(ISprint1Task7V9), (5.982, new object[] {2, 4 })},
            { typeof(ISprint1Task7V10), (6.398, new object[] {20, 20 })},
            { typeof(ISprint1Task7V11), (15.527, new object[] {5, 5 })},
            { typeof(ISprint1Task7V12), (2257.593, new object[] {5, 5 })},
            { typeof(ISprint1Task7V13), (0.999, new object[] {156, 156 })},
            { typeof(ISprint1Task7V14), (-0.889, new object[] {5, 5 })},
            { typeof(ISprint1Task7V15), (96.484, new object[] {5 })},
            
        };
        public (object result, object[] args) GetData(Type type)
        {
            return testData[type];
        }

    }
}

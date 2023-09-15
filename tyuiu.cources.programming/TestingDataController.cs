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

            { typeof(ISprint1Task0V1), (2.0, new object[] {})},
            { typeof(ISprint1Task1V0), (2.0, new object[] {1, 9})},
            { typeof(ISprint1Task2V0), (2, new object[] {2})},
            { typeof(ISprint1Task3V0), (2.0, new object[] {1, 1})},
            { typeof(ISprint1Task3V8), (54.794520547945204, new object[] {5000, 10, 40})},
            { typeof(ISprint1Task3V9), (3.0, new object[] {200})},
            { typeof(ISprint1Task3V10), (30.5, new object[] {30.5})},
            { typeof(ISprint1Task3V11), (19.0, new object[] {-2, 5, 1, 7, 5, -3})},
            { typeof(ISprint1Task3V12), (72.0, new object[] {12, 12})},
            { typeof(ISprint1Task3V13), (6.0, new object[] {123})},
            { typeof(ISprint1Task3V14), (321.0, new object[] {123})},
            { typeof(ISprint1Task3V15), (15.0, new object[] {1, 5, 3, 2})},
            { typeof(ISprint1Task3V16), (-2.0, new object[] {1, 1})},
            { typeof(ISprint1Task3V17), ("true", new object[] {1})},
            { typeof(ISprint1Task3V18), (9.0, new object[] {9, 9, 3})},
            { typeof(ISprint1Task3V19), ("true", new object[] {5, 1, 5, 1})},
            { typeof(ISprint1Task4V0), (2.0, new object[] {1 , 1 })},
            { typeof(ISprint1Task4V11), (0.009253901576178851, new object[] {5, 5 })},
            { typeof(ISprint1Task4V12), (0.6024639000089176, new object[] {0.5 , 4 })},
            { typeof(ISprint1Task4V13), (-0.0451117610788709, new object[] {1 , 1 })},
            { typeof(ISprint1Task4V14), (0.007055336829505575, new object[] {5 , 5 })},
            { typeof(ISprint1Task4V15), (1969799074.1199136, new object[] {5 , 5 })},
            { typeof(ISprint1Task4V16), (0.037037037037037035, new object[] {23})},
            { typeof(ISprint1Task4V17), (0.11547005383792514, new object[] {100 , 5 })},
            { typeof(ISprint1Task4V18), (0,004525483399593904, new object[] {5 , 5 })},
            { typeof(ISprint1Task4V19), (1.14, new object[] {52 , 5 })},
            { typeof(ISprint1Task4V20), (2.5485837703548637, new object[] {5 , 5 })},
            { typeof(ISprint1Task4V21), (41.011111111111, new object[] {60, 30 })},
            { typeof(ISprint1Task4V22), (0,00185179469769451, new object[] {120, 30 })},
            { typeof(ISprint1Task4V23), (0.0960916767552923, new object[] {60, 30 })},
            { typeof(ISprint1Task4V24), (1.19920575776058, new object[] {60, 30 })},
            { typeof(ISprint1Task4V25), (0,551212925417111, new object[] {120 })},
            { typeof(ISprint1Task4V26), (-0.0619372147447247, new object[] {1 , 1 })},
            { typeof(ISprint1Task4V27), (0.151842154231824, new object[] {8, 2 })},
            { typeof(ISprint1Task4V28), (0,367879441171442, new object[] {0, 1 })},
            { typeof(ISprint1Task4V29), (0,0235702260395515, new object[] {10, 2 })},
            { typeof(ISprint1Task4V30), (10.0, new object[] {2, 2 })},
            { typeof(ISprint1Task5V4), (5.0, new object[] {20000})},
            { typeof(ISprint1Task5V5), (6.0, new object[] {50.699})},
            { typeof(ISprint1Task5V6), (5.0, new object[] {5})},
            { typeof(ISprint1Task5V7), (1.0, new object[] {45})},
            { typeof(ISprint1Task6V0), ("", new object[] {"" })},
            { typeof(ISprint1Task6V7), ("привет ми", new object[] {"привет мир" })},
            { typeof(ISprint1Task6V8), ("риветп ирм", new object[] {"привет мир" })},
            { typeof(ISprint1Task6V9), ("тприве рми", new object[] {"привет мир" })},
            { typeof(ISprint1Task6V10), ("привет мр", new object[] {"привет мир" })},
            { typeof(ISprint1Task6V11), ("false", new object[] {"привет мир" })},
            { typeof(ISprint1Task6V12), ("true", new object[] {"привет мир мир" })},
            { typeof(ISprint1Task6V13), ("true", new object[] {"абвгд" })},
            { typeof(ISprint1Task6V14), ("true", new object[] {"строка" })},
            { typeof(ISprint1Task6V15), ("true", new object[] {"привет!" })},
            { typeof(ISprint1Task6V16), ("true", new object[] {"что?!" })},
            { typeof(ISprint1Task6V17), ("true", new object[] {"шалаш" })},
            { typeof(ISprint1Task6V18), ("true", new object[] {"122" })},
            { typeof(ISprint1Task7V0), (2.0, new object[] {1, 1, 1 })},
            { typeof(ISprint1Task7V11), (15.52786404500042, new object[] {20, 20})},
            { typeof(ISprint1Task7V12), (2257.59375, new object[] {5, 5})},
            { typeof(ISprint1Task7V13), (0.9999406539380555, new object[] {156, 156})},
            { typeof(ISprint1Task7V14), (-0.888994332234069, new object[] {5, 5 })},
            { typeof(ISprint1Task7V15), (96.48424570975712, new object[] {5})},
            { typeof(ISprint1Task7V16), (0.026361372404058114, new object[] {5 })},
            { typeof(ISprint1Task7V17), (0.08651362062635382, new object[] {5, 5 })},
            { typeof(ISprint1Task7V18), (5.000781067023561, new object[] {5, 5 })},
            { typeof(ISprint1Task7V19), (2502.641075725337, new object[] {5 })},
            { typeof(ISprint1Task7V20), (25.47896594869075, new object[] {5, 5 })},
            { typeof(ISprint1Task7V21), (-18.9167076610623, new object[] {2, 4})},
            { typeof(ISprint1Task7V22), (0.172352094070684, new object[] {2, 4 })},
            { typeof(ISprint1Task7V23), (-1.78183301889364, new object[] {2, 4 })},
            { typeof(ISprint1Task7V24), (0.0160435112198901, new object[] {2, 4 })},
            { typeof(ISprint1Task7V25), (6.73348074646769, new object[] {2, 4 })},
            { typeof(ISprint1Task7V26), (0.753902658833151, new object[] {2, 4 })},
            { typeof(ISprint1Task7V27), (-3.59725612735524, new object[] {2, 4 })},
            { typeof(ISprint1Task7V28), (16.7872303975994, new object[] {2, 4 })},
            { typeof(ISprint1Task7V29), (2.07151744378031, new object[] {2, 4 })},
            { typeof(ISprint1Task7V30), (11,9776146190686, new object[] {2, 4 })},
        };
        public (object result, object[] args) GetData(Type type)
        {
            return testData[type];
        }

        public string ReverseString(string str)
        {
            return "54321";
        }
    }
}

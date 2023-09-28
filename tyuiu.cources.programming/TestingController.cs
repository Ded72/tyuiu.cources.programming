using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming
{
    public class TestingController
    {

        private readonly TestingDataController testDataController;

        public TestingController(TestingDataController testDataController)
        {
            this.testDataController = testDataController;
        }
        public (bool IsSuccess, IEnumerable<String> lines) Run<T>(T instance)
        {
            MethodInfo method = GetInstanceMethod(instance);
            var data = testDataController.GetData(instance.GetType().GetInterfaces().First());
            var res = RunMethod(instance, method, data);
            Console.WriteLine($"Successful launch: { instance.GetType().FullName} !");
            return (AreEquals(data.result, res), GetReport(instance, method, data, res));
        }
        private static MethodInfo GetInstanceMethod<T>(T? instance)
        {

            return instance!.GetType().GetMethods().First();
        }

        private object? RunMethod<T>(T? instance, MethodInfo method, (object result, object[] args) data)
        {
            return method.Invoke(instance, data.args);
        }
        private IEnumerable<string> GetReport<T>(T instance, MethodInfo method, (object result, object[] args) data, object? res)
        {
            var outList = new List<string>();
            var buffer = $"{data.result.GetType().Name} {method.Name}(";
            foreach (var param in method.GetParameters())
            {
                buffer += $"{param.ParameterType.Name} {param.Name}, ";
            }
            buffer = buffer.Substring(0, buffer.Length - 2) + ")";
            outList.Add($"{buffer}");

            outList.Add("Parameters:");
            var argsNameAndVal = data.args.Zip(method.GetParameters(), (first, second) => $"{second}={first}");
            foreach (var p in argsNameAndVal)
            {
                outList.Add(p);
            }

            outList.Add("  Result:");
            outList.Add(WriteValue($"    real: ", res));
            outList.Add(WriteValue($"expected: ", data.result));
            outList.Add(AreEquals(data.result, res) ? "VALID" : "FAIL");
            outList.Add("--------------------------------");
            return outList;
        }
        private string WriteValue(string message, object? value)
        {
            var buffer = message;
            if (value is Array valueArray)
            {             
                foreach (var item in valueArray)
                {
                    buffer += $"{item} ";
                }
            }
            else if (value!.GetType() == typeof(string))
            {
                buffer += $"'{value}'";
            }
            else
            {
                buffer += $"{value}";
            }
            return buffer;
        }
        private bool AreEquals(object? expected, object? res)
        {
            if (res is Array resArray && expected is Array expectedArray)
            {
                if (resArray.Length != expectedArray.Length)
                {
                    return false;
                }

                for (int i = 0; i < resArray.Length; i++)
                {
                    if (!object.Equals(resArray.GetValue(i), expectedArray.GetValue(i)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return res!.Equals(expected);
            }
        }

    }
}

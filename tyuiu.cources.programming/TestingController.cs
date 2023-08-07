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
        private readonly AssemblyController assemblyController;
        private readonly TestDataController testDataController;

        public TestingController(AssemblyController assemblyController, TestDataController testDataController) 
        {
            this.assemblyController = assemblyController;
            this.testDataController = testDataController;
        }
        public IEnumerable<String> Run<T>(string filename)
        {
            var instance = assemblyController.CreateInstanceFromFile<T>(filename);
            MethodInfo method = GetInstanceMethod(instance);
            var data = testDataController.GetData<T>();
            var res = RunMethod(instance, method, data);
            return GetReport<T>(instance, method, data, res);
        }

        private static MethodInfo GetInstanceMethod<T>(T? instance)
        {
            return instance!.GetType().GetInterface(typeof(T).Name)!.GetMethods().First();
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
            outList.Add(AreEquals(data.result, res)?"VALID":"FAIL");
            outList.Add("--------------------------------");
            return outList;
        }
        private string WriteValue(string message, object? value)
        {
            var buffer = message;
            if (value!.GetType().IsArray)
            {
                foreach (var item in (int[])value!)
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
            if (res!.GetType().IsArray)
            {
                if (res!.GetType().GetElementType()?.Name == "Int32")
                {
                    return ((int[])res).Length == ((int[])expected!).Length;
                }
                else { return false; }
            }
            else
            {
                return res!.Equals(expected);
            }
        }
    }
}

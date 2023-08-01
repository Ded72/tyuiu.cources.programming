﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.Interfaces;

namespace tyuiu.cources.programming
{
    public class TestController
    {
        private readonly AssemblyController assemblyController;
        private readonly IOutputController output;
        private Dictionary<Type, (object result, object[] args)> testData;

        public TestController(AssemblyController assemblyController, IOutputController output)
        {
            this.assemblyController = assemblyController;
            this.output = output;
            this.testData = BuildTestData();
        }
        public bool Run<T>(string filename)
        {
            T cls = assemblyController.LoadFromFile<T>(filename);
            var method = cls!.GetType().GetInterface(typeof(T).Name)!.GetMethods().First();
            var argsData = GetTestingData<T>();
            var res = method.Invoke(cls, argsData.args);

            OutResult(method, argsData, res);

            return res!.Equals(argsData.result) || res == argsData.result;
        }

        private void OutResult(MethodInfo method, (object result, object[] args) argsData, object? res)
        {
            var buffer = $"{method.Name}(";
            foreach (var param in method.GetParameters())
            {
                buffer += $"{param.ParameterType.Name} {param.Name}, ";
            }
            buffer = buffer.Substring(0, buffer.Length-2) + ")\n";
            output.WriteLine(buffer);

            output.WriteLine("Parameters:");
            var ppp = argsData.args.Zip(method.GetParameters(), (first, second) => $"{second}={first}");
            foreach(var p in ppp)
            {
                output.WriteLine(p);
            }

            output.WriteLine($"expected {argsData.result} real {res}");
        }

        private (object result, object[] args) GetTestingData<T>()
        {
            return testData[typeof(T)];
        }
        private Dictionary<Type, (object result, object[] args)> BuildTestData()
        {
            return new Dictionary<Type, (object result, object[] args)>() {
                { typeof(ISprint0Task0V0), ("54321", new object[] { "12345" })},
                { typeof(ISprint0Task0V1), (90, new object[] { 10 })},
                { typeof(ISprint0Task0V2), (20, new object[] { 10, 2 })},
                { typeof(ISprint1Task0V0), (
                    new int[] { 1, 2, 3 }, 
                    new object[] { new int[] { 2, 3, 1 } 
                })},
            };
        }

    }
}

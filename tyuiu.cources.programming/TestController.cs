using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming
{
    public class TestController
    {
        private readonly AssemblyController assemblyController;
        private Dictionary<Type, Func<object, bool>>? testMethods;

        public TestController(AssemblyController assemblyController)
        {
            this.assemblyController = assemblyController;
            this.testMethods = BuildTestMethods();
        }
        public bool? Test<T>(string filename)
        {
            T cls = assemblyController.LoadFromFile<T>(filename);
            return testMethods?[typeof(T)](cls!);
        }
        private Dictionary<Type, Func<object, bool>> BuildTestMethods()
        {
            return new Dictionary<Type, Func<object, bool>>() {
                {
                    typeof(ISprint0Task0V0),
                    (cls) => {
                        return "54321"==(cls as ISprint0Task0V0)!.ReverseString("12345");
                    }
                },
                {
                    typeof(ISprint0Task0V1),
                    (cls) => {
                        return 90==(cls as ISprint0Task0V1)!.SubFrom100(10);
                    }
                },
                {
                    typeof(ISprint0Task0V2),
                    (cls) => {
                        return 20==(cls as ISprint0Task0V2)!.Multiply(10, 2);
                    }
                }
            };
        }

    }
}

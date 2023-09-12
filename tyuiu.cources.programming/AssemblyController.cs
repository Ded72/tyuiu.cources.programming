using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace tyuiu.cources.programming
{
    public class AssemblyController
    {
        //public T CreateInstanceFromByteArray<T>(byte[] buffer)
        //{
        //    var assembly = Assembly.Load(buffer);
        //    return CreateInstance<T>(assembly);
        //}
        //public T CreateInstanceFromStream<T>(Stream stream)
        //{
        //    var assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
        //    return CreateInstance<T>(assembly);
        //}
        //public T CreateInstanceFromFile<T>(string filename)
        //{
        //    if (!File.Exists(filename)) { throw new FileNotFoundException($"File {filename} not found!"); }
        //    var assembly = Assembly.LoadFile(filename);
        //    return CreateInstance<T>(assembly);
        //}

        public object CreateInstanceFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File {filename} not found!");
            }
            var assembly = Assembly.LoadFile(filename);
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    var interfs = type.GetInterfaces();
                    foreach (var intf in interfs)
                    {
                        if (intf != null)
                        {
                            //Console.WriteLine(intf.FullName);
                            return CreateInstance(assembly, intf.FullName!)!;
                        }
                    }
                    throw new InvalidOperationException($"No interface found in {filename} the assembly!");
                }
            }
            throw new InvalidOperationException($"No class found in {filename} the assembly!");
        }

        private object CreateInstance(Assembly assembly, string interfaceName)
        {
            var cls = assembly.GetTypes()
                .Where(type => type.IsClass && type.GetInterface(interfaceName) != null)
                .First();
            
            return Activator.CreateInstance(cls);
        }

    }
}
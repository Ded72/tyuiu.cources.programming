using System.Reflection;

namespace tyuiu.cources.programming
{
    public class TestController
    {
        public T LoadTestingInterface<T>(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File {filename} not found!");
            }
            var assembly = Assembly.LoadFrom(filename);
            var cls = assembly.GetTypes()
                .Where(cls => cls.IsClass && cls.GetInterfaces().Contains(typeof(T)))
                .First();
            if(cls == null)
            {
                throw new NullReferenceException();
            }
            return (T)Activator.CreateInstance(cls!)!;
        }
    }
}
using LibGit2Sharp;
using System.Diagnostics;
using System.IO;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using tyuiu.SidorovAY.Sprint0.Task0.V0;
using Moq;


namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class CsvControllerTests
    {

        [TestMethod]
        public void LoadFileValid()
        {
            var csvController = new CsvController(new ScoreController(), 
                new GitController(@"C:\Temp"), 
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            var testingController = new TestingController(new TestingDataController());

            List<object> interfacesFromDll = csvController.Load(@"C:\Temp\links.csv");
            foreach (var intf in interfacesFromDll)
            {
                //Console.WriteLine(intf.GetType().GetInterfaces().First().Name);
                var result = testingController.Run(intf);

            }
            ;

        }

        

    }
}



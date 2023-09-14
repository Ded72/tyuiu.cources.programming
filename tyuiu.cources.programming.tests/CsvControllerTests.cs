using LibGit2Sharp;
using System.Diagnostics;
using System.IO;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using Moq;


namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class CsvControllerTests
    {

        [TestMethod]
        public void LoadFileValid()
        {
            var csvController = new CsvController( 
                new GitController(@"C:\Temp"), 
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            var testingController = new TestingController(new TestingDataController());

            string studentResults = csvController.Load(@"C:\Temp\0_7_Vyslat_ssylku_s_GitHub-otvety.csv");
            Console.WriteLine(studentResults);

            

        }

        

    }
}



using LibGit2Sharp;
using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;



namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class GitControllerTests
    {
        [TestMethod]
        public void LoadRepositoryToDirValid()
        {

            var gitController = new GitController();
            var csvController = new CsvController();
            string [,] links = csvController.ReadLinksFromCsv();
            //gitController.Load("https://github.com/Andrey-1970/git_prj.git", @"c:\temp");
            for (int i = 1; i < links.GetLength(0); i++) {
                //Console.WriteLine(links[i, 0]);
                //Console.WriteLine(links.GetLength(0));
                gitController.Load(links[i, 0], @"c:\temp");
                
            }
               
           
            
            
        }

       }
}

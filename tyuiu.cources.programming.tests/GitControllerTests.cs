using LibGit2Sharp;
using tyuiu.cources.programming.interfaces;




namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class GitControllerTests
    {
        [TestMethod]
        public void LoadRepositoryToDirValid()
        {
            string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
            var gitController = new GitController(@"C:\Temp");
            gitController.Load("https://github.com/clipboard1/tyuiu.cources.programming", currentDate);
        }

       }
}

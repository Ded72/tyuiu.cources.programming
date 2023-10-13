using LibGit2Sharp;
using tyuiu.cources.programming.interfaces;




namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class GitControllerTests
    {
        [TestMethod]
        public void LoadRepositoryValid()
        {
            string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
            var gitController = new GitController(@"F:\ServiceWorkFolder");
            string repoUrl = "https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1";
            bool loaded = gitController.Load(repoUrl, currentDate);
            Assert.AreEqual(true, loaded);
        }

        [TestMethod]
        public void LoadRepositoryFail()
        {
            string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
            var gitController = new GitController(@"F:\ServiceWorkFolder");
            string repoUrl = "https://github.com/clipboard1/Tyuiu.SimonSSTests.Sprint1";
            bool loaded = gitController.Load(repoUrl, currentDate);
            Assert.AreEqual(false, loaded);
        }

    }
}

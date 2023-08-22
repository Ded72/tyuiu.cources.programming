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
            //gitController.Load("https://github.com/Andrey-1970/git_prj.git", @"c:\temp");
            gitController.Load(
                "https://github.com/Andrey-1970/tyuiu.SpirinIS.Sprint0.Task0.V2.git",
                @"c:\temp");
        }
    }
}

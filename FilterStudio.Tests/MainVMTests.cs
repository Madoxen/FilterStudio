using FilterStudio.VM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilterStudio.Tests
{
    [TestClass]
    public class MainVMTests
    {
        [TestMethod]
        public void TestForNullValuesWithDefaultCtor()
        {
            MainVM main = new MainVM();

            Assert.IsNotNull(main.AddFilterCommand);
            Assert.IsNotNull(main.CreateNewProjectCommand);
            Assert.IsNotNull(main.LoadProjectCommand);
            Assert.IsNotNull(main.SaveProjectCommand);
            Assert.IsNotNull(main.RemoveFilterCommand);
            Assert.IsNotNull(main.ExecutionEngineVM);
        }

       
    }
}

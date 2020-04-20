using FilterStudio.VM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Tests
{
    [TestClass]
    public class ExecutionVMTests
    {
        [TestMethod]
        public void TestForNullValuesWithDefaultCtor()
        {
            ExecutionEngineVM vm = new ExecutionEngineVM(new System.Collections.ObjectModel.ObservableCollection<FilterVM>());
            Assert.IsNotNull(vm.ExecuteTreeCommand);
            Assert.IsNotNull(vm.LoadImageCommand);
            Assert.IsNotNull(vm.SaveImageCommand);

        }
    }
}

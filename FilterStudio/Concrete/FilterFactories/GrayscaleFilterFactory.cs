using FilterStudio.Interfaces;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Concrete.FilterFactories
{
    public class GrayscaleFilterFactory : IFilterFactory
    {
        public FilterVM CreateNewFilter()
        {
            GrayscaleFilter concrete = new GrayscaleFilter();
            FilterVM vm = new FilterVM(concrete);
            vm.Name = "New Grayscale Filter";
            return vm;
        }
    }
}

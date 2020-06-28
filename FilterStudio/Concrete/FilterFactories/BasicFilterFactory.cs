using FilterStudio.Interfaces;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Concrete.FilterFactories
{
    internal class BasicFilterFactory : IFilterFactory
    {
        public FilterVM CreateNewFilter()
        {
            BasicMatrixFilter concreteFilter = new BasicMatrixFilter
           (new double[3, 3] {
                { 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0 }});


            FilterVM vm = new FilterVM(concreteFilter);
            vm.Name = "New Basic Filter";
            vm.DataVM = new BasicMatrixFilterDataProviderVM(vm);

            return vm;
        }
    }
}

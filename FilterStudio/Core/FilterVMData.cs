using FilterStudio.Interfaces;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Core
{
    /// <summary>
    /// Class that holds serializable data
    /// </summary>
    public class FilterVMData
    {
        public bool CanReorder;
        public bool CanDelete;
        public IFilter UnderlayingFilter;
        public Type FilterDataProviderType;
        public string Name;

        public FilterVMData()
        { }

        public FilterVMData(FilterVM vm)
        {
            CanReorder = vm.CanReorder;
            CanDelete = vm.CanDelete;
            UnderlayingFilter = vm.UnderlayingFilter;
            FilterDataProviderType = vm.DataVM.GetType();
            Name = vm.Name;
        }

    }
}

using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.VM
{

    /// <summary>
    /// Exposes Underlaying Filter data in a format friendly for binding
    /// Base class for all data providers
    /// </summary>
    public class FilterDataProviderVM : BaseVM
    {
        /// <summary>
        /// Underlaying filter reference, used to set data values in concrete filter
        /// </summary>
        protected FilterVM filter;

        public FilterDataProviderVM(FilterVM filter)
        {
            this.filter = filter;
        }


        public virtual void SetData() { }
    }
}

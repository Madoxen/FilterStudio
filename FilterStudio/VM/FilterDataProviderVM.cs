using FilterStudio.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.VM
{

    /// <summary>
    /// Exposes Underlaying Filter data in a format friendly for binding
    /// Base class for all data providers
    /// </summary>
    public abstract class FilterDataProviderVM : BaseVM
    {
        /// <summary>
        /// Underlaying filter reference, used to set data values in concrete filter
        /// </summary>
        [JsonIgnore]
        protected FilterVM filter;

        [JsonConstructor]
        protected FilterDataProviderVM() { }

        public FilterDataProviderVM(FilterVM filter)
        {
            this.filter = filter;
        }


        public abstract void SetData();
        public abstract void CopySettings(FilterDataProviderVM provider);
    }
}

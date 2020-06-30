using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    public interface IFilterFactory
    {
        /// <summary>
        /// Creates new predifined Filter that is ready to use
        /// </summary>
        /// <returns></returns>
        FilterVM CreateNewFilter();
    }
}

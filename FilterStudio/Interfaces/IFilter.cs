using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents an 
    /// </summary>
    public interface IFilter
    {
        

        /// <summary>
        /// Layer that this filter is in. Values of subsequent images are pushed from 0 -> Max 
        /// </summary>
        int Layer { get; set; }


        /// <summary>
        /// An Operation Method that will execute code in concrete implementation
        /// using Inputs provided values and after execution, will push necessary values to outputs
        /// </summary>
        void Operate();

        /// <summary>
        /// Value that will be pushed after Operate 
        /// </summary>
        Image Output { get; }

        /// <summary>
        /// Value that is pushed before Operate
        /// </summary>
        Image Input { get; set; }
    }
}

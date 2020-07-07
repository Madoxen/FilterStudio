using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents an operation unit (strategy pattern)
    /// </summary>
    public interface IFilter
    { 
        /// <summary>
        /// An Operation Method that will execute code in concrete implementation
        /// using Inputs provided values and after execution, will push necessary values to outputs
        /// </summary>
        void Operate();

        /// <summary>
        /// Value that will be pushed after Operate 
        /// </summary>
        Bitmap Output { get; }

        /// <summary>
        /// Value that is pushed before Operate
        /// </summary>
        Bitmap Input { get; set; }
    }
}

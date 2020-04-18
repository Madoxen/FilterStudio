using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents an operation node in the Project tree
    /// 
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Input sockets declared by this Operation
        /// </summary>
        IInputSocket[] Inputs { get; set; }

        /// <summary>
        /// Output sockets declared by this Operation
        /// </summary>
        IOutputSocket[] Outputs { get; set; }

        /// <summary>
        /// An Operation Method that will execute code in concrete implementation
        /// using Inputs provided values and after execution, will push necessary values to outputs
        /// </summary>
        void Operate();
    }
}

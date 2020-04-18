using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents connection from output sockets to input sockets
    /// </summary>
    public interface IConnection
    { 
        /// <summary>
        /// Output socket to take values from
        /// </summary>
        IOutputSocket From { get; set; }
        
        /// <summary>
        /// Input sockets to push value to
        /// </summary>
        IInputSocket To { get; set; }
    }
}

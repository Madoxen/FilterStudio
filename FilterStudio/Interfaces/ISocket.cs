using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Describes generic socket
    /// </summary>
    public interface ISocket
    {

        /// <summary>
        /// An Operation Node that this socket is attached to
        /// </summary>
        IOperation Operation { get; }
    }
}

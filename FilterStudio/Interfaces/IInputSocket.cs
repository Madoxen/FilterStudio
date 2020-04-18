using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents Input Socket that is attached to IOperation
    /// Only one incoming connection can be attached
    /// </summary>
    public interface IInputSocket : ISocket
    {
        /// <summary>
        /// Type that is accepted by this Input Socket
        /// </summary>
        Type TypeOf { get; set; }

        /// <summary>
        /// An Operation Node that this socket is attached to
        /// </summary>
        IOperation Operation { get; }
    }
}

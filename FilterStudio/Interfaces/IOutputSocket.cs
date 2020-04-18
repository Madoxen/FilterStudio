using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Interfaces
{
    /// <summary>
    /// Represents outputing socket that can be attached to the Operation Node
    /// </summary>
    public interface IOutputSocket : ISocket
    {
        /// <summary>
        /// Type that is accepted by this Output Socket
        /// </summary>
        Type TypeOf { get; set; }

    }

}


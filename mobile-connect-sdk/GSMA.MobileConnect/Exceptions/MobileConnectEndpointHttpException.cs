﻿using System;

namespace GSMA.MobileConnect.Exceptions
{
    /// <summary>
    /// Exception raised when calls to the discovery endpoint encounter a http exception such as unreachable host
    /// </summary>
    public class MobileConnectEndpointHttpException : Exception
    {
        /// <inheritdoc/>
        public MobileConnectEndpointHttpException() { }
        /// <inheritdoc/>
        public MobileConnectEndpointHttpException(string message) : base(message) { }
        /// <inheritdoc/>
        public MobileConnectEndpointHttpException(string message, Exception inner) : base(message, inner) { }
    }
}

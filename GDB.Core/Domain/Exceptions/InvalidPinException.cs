using System;
using GDB.Core.Domain.Exceptions;


namespace GDB.Core.Domain.Exceptions
{
    /// <summary>
    /// Purpose: Thrown when entered PIN does not match account PIN.
    /// </summary>
    public class InvalidPinException : AccountException
    {
        public InvalidPinException(string message = "") : base(message) { }
    }
}

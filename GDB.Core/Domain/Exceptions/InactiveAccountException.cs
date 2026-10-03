using System;
using GDB.Core.Domain.Exceptions;


namespace GDB.Core.Domain.Exceptions
{
    /// <summary>
    /// Purpose: Thrown when an operation is attempted on an inactive account.
    /// </summary>
    public class InactiveAccountException : AccountException
    {
        public InactiveAccountException(string message = "") : base(message) { }
    }
}

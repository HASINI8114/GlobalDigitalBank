using System;
using GDB.Core.Domain.Exceptions;

namespace GDB.Core.Domain.Exceptions
{
    /// <summary>
    /// Purpose: Thrown when withdrawal amount exceeds available funds.
    /// </summary>
    public class InsufficientBalanceException : AccountException
    {
        public InsufficientBalanceException(string message = "") : base(message) { }
    }
}

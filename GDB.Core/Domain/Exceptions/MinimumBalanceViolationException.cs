using System;
using GDB.Core.Domain.Exceptions;


namespace GDB.Core.Domain.Exceptions
{
    /// <summary>
    /// Purpose: Thrown when withdrawal breaches minimum balance requirement.
    /// </summary>
    public class MinimumBalanceViolationException : AccountException
    {
        public MinimumBalanceViolationException(string message = "") : base(message) { }
    }
}

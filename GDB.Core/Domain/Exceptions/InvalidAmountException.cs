using System;
using GDB.Core.Domain.Exceptions;


namespace GDB.Core.Domain.Exceptions
{
    /// <summary>
    /// Purpose: Thrown when deposit or withdrawal amount <= 0.
    /// </summary>
    public class InvalidAmountException : AccountException
    {
        public InvalidAmountException(string message = "") : base(message) { }
    }
}

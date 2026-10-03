using System;
using GDB.Core.Domain;
using GDB.Core.Domain.Enums;
using GDB.Core.Domain.Exceptions;
namespace GDB.Core.Domain.Models

{
    public interface IAccount
    {
        string AccountNumber { get; }
        string Name { get; }
        int Age { get; }
        decimal Balance { get; }
        AccountType AccountType { get; }
        AccountStatus Status { get; }

        AccountPrivilege Privilege { get; }


        bool CheckIfAccountIsActive(); 
        void Deposit(decimal amount);
        void Withdraw(decimal amount, string enteredPin);
        bool ValidatePin(string enteredPin);

        bool ChangePin(string oldPIn, string newPin);

    }
}

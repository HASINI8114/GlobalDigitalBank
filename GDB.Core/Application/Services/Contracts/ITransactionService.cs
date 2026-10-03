using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDB.Core.Application.Dtos;
using GDB.Core.Domain.Enums;

namespace GDB.Core.Application.Services.Contracts
{
    public interface ITransactionService
    {
        Task<TResponse> ProcessTransactionAsync<TResponse>(
            TransactionDto transactionDto,
            TransactionType transactionType);
    }
}

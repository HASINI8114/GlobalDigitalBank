using GDB.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Services.Contracts
{
    public interface ITransactionCommand<TResponse>
    {
        Task<TResponse> ExecuteAsync(
            TransactionDto transactionDto);
    }
}

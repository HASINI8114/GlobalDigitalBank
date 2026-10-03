using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDB.Core.Domain.Enums;

namespace GDB.Core.Application.Dtos
{
    public class DepositResponseDto
    {
        public decimal Balance { get; set; }

        public TransactionStatus TransactionStat { get; set; }
    }
}

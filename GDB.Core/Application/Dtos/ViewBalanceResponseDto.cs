using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Dtos
{
    public class ViewBalanceResponseDto
    {
        public string AccountNumber { get; set; }
        
        public decimal Balance { get; set; }
    }
}

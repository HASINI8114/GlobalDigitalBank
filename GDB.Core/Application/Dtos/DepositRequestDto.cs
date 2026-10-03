using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Dtos
{
   
        public class DepositRequestDto
        {
            [Required]
            [StringLength(32)]
            public string AccountNumber { get; set; }

        [Range(typeof(decimal), "0.01", "10000000",
        ErrorMessage = "Amount must be between 0.01 and 10000000.")]
        public decimal Amount { get; set; }
    }
    
}

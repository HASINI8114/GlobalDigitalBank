using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Dtos
{
    public class TransferRequestDto
    {
        [Required]
        [StringLength(32)]
        public string FromAccount { get; set; }

        [Required]
        [StringLength(32)]
        public string ToAccount { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$",
            ErrorMessage = "PIN must contain exactly 4 digits.")]
        public string Pin { get; set; }

        [Range(typeof(decimal), "0.01", "10000000",
    ErrorMessage = "Amount must be between 0.01 and 10000000.")]
        public decimal Amount { get; set; }
    }
}

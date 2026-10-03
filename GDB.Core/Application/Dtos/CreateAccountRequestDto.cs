using GDB.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Dtos
{
    public class CreateAccountRequestDto
    {
        [Required]
        [StringLength(32)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$",
            ErrorMessage = "PIN must contain exactly 4 digits.")]
        public string Pin { get; set; }

        [EnumDataType(typeof(AccountType))]
        public AccountType AccountType { get; set; }

        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus Status { get; set; }

        [EnumDataType(typeof(AccountPrivilege))]
        public AccountPrivilege Privilege { get; set; }

        public decimal OverdraftLimit { get; set; }

        public int TenureMonths { get; set; }

        public double InterestRate { get; set; }

        public decimal MinimumBalance { get; set; }

        [StringLength(100)]
        public string EmployerName { get; set; }
    }
}

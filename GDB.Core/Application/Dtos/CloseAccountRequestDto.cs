using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Application.Dtos
{
    public class CloseAccountRequestDto
    {
        [Required]
        [StringLength(32)]
        public string AccountNumber { get; set; }

    }
}

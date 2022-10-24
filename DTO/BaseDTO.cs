using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaseDTO
    {
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Id { get; set; }
    }
}

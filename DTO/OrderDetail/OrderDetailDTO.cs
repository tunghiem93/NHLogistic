using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Order
{
    public class OrderDetailDTO : BaseDTO
    {
        public string Order_no_id { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Items { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public int Quantity { get; set; }
    }
}

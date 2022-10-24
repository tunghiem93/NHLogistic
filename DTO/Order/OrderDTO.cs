using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Order
{
    public class OrderDTO : BaseDTO
    {
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Order_no { get; set; }
        public string Customer_name { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Receive_address { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Total_amount { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc.")]
        public string Note { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public OrderDTO()
        {
            this.OrderDetails = new List<OrderDetailDTO>();
        }
    }
}

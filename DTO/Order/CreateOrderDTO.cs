using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Order
{
    public class CreateOrderDTO : OrderDTO
    {
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public CreateOrderDTO()
        {
            this.OrderDetails = new List<OrderDetailDTO>();
        }
    }
}

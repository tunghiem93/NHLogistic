using DTO;
using DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IOrderManagement
    {
        void Create(CreateOrderDTO input);
        void Update(UpdateOrderDTO input, string id);
        void Delete(string id);
        OrderDTO GetById(string id);
        PaginationModels<List<OrderDTO>> Get(string search);
    }
}

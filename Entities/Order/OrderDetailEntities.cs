using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Order
{
    [Table("OrderDetails")]
    public class OrderDetailEntities : BaseEntities
    {
        public string Order_no_id { get; set; }
        public string Items { get; set; }
        public int Quantity { get; set; }
        public virtual OrderEntities Order { get; set; }
    }
}

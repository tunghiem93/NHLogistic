using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Order
{
    [Table("Orders")]
    public class OrderEntities : BaseEntities
    {
        public string Order_no { get; set; }
        public string Customer_name { get; set; }
        public string Receive_address { get; set; }
        public string Total_amount { get; set; }
        public string Note { get; set; }
        public virtual ICollection<OrderDetailEntities> OrderDetails { get; set; }
    }
}

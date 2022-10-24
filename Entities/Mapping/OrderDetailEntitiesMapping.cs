using Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Mapping
{
    public class OrderDetailEntitiesMapping : EntityTypeConfiguration<OrderDetailEntities>
    {
        public OrderDetailEntitiesMapping()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(x => x.Order_no_id).HasColumnType("varchar").HasMaxLength(20);
            this.Property(x => x.Items).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            this.Property(x => x.Quantity).IsRequired();

            this.HasOptional(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.Order_no_id);
        }
    }
}

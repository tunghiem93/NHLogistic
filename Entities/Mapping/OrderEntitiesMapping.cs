using Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Mapping
{
    public class OrderEntitiesMapping : EntityTypeConfiguration<OrderEntities>
    {
        public OrderEntitiesMapping()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(x => x.Order_no).HasColumnType("varchar").HasMaxLength(20);
            this.Property(x => x.Customer_name).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(x => x.Receive_address).HasColumnType("nvarchar").HasMaxLength(160).IsRequired();
            this.Property(x => x.Total_amount).IsRequired();
            this.Property(x => x.Note).HasColumnType("nvarchar").HasMaxLength(160);
        }
    }
}
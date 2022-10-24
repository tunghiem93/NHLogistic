using Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=NHLogisticDbContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        public virtual DbSet<OrderEntities> Orders { get; set; }
        public virtual DbSet<OrderDetailEntities> OrderDetails { get; set; }
    }
}

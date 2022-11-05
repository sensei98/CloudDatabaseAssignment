using Newtonsoft.Json;
using OnlineStore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class Order: IEntityBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}

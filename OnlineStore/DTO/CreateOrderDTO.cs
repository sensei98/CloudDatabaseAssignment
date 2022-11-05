using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}

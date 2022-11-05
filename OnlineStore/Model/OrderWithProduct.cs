using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class OrderWithProduct
    {
        public Guid OrderId { get; set; }
        public Product ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public OrderWithProduct()
        {

        }
        //public OrderWithProduct(Guid orderId, Guid productId)
        //{
        //    OrderId = orderId;
        //    ProductId = productId;
        //}

    }
}

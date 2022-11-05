using OnlineStore.DTO;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetSingleOrderById(Guid id);
        Task AddOrder(CreateOrderDTO order);
        Task UpdateOrderById(Guid id, Order order);
        Task DeleteSingleOrderById(Guid id);
        ICollection<Product> GetMultipleProducts(ICollection<Guid> products);
    }
}

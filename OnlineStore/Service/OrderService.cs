using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL;
using OnlineStore.DTO;
using OnlineStore.Interface;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly OnlineStoreContext _context;
        public OrderService(OnlineStoreContext context)
        {
            _context = context;
        }
        public async Task AddOrder(CreateOrderDTO order)
        {           
            Order newOrder = new Order()
            {
                Id = Guid.NewGuid(),
                Address = order.Address,
                OrderDate = DateTime.UtcNow,
                Products = new List<Product>(),
                UserId = order.UserId,
                ShippingDate = DateTime.UtcNow.AddDays(2) //shipped two days after order is placed
            };

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSingleOrderById(Guid id)
        {
            _context.Orders.Remove(new Order { Id = id});
            await _context.SaveChangesAsync();
        }

        public ICollection<Product> GetMultipleProducts(ICollection<Guid> products)
        {
            return _context.Products.Where(c => products.Contains(c.Id)).ToList();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetSingleOrderById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task UpdateOrderById(Guid id, Order order)
        {
            order.Id = id;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}

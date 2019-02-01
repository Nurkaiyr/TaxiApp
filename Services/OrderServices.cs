using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderServices
    {
        private readonly TaxiContext _context;

        public OrderServices(TaxiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrder()
        {
            var order = _context.Orders.Select(orders => new Order
            {
                StartPoint = orders.StartPoint,
                EndPoint = orders.EndPoint,
                Id = orders.Id,
                Price = orders.Price
            });

            return await Task.FromResult(order);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var newOrder = new Order
            {
                StartPoint = order.StartPoint,
                EndPoint = order.EndPoint,
                Price = order.Price
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }

        public async Task<Order> Taken(int id)
        {
            var takeOrder = await _context.Orders.Where(order=>order.OrderStatus == 0).SingleOrDefaultAsync(order => order.Id == id);
            takeOrder.OrderStatus = OrderStatus.Taken;

            _context.Orders.Update(takeOrder);
            await _context.SaveChangesAsync();

            return takeOrder;
        }
    }
}

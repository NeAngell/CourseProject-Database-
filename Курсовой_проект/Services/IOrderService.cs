using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface IOrderService
    {
        void AddOrder(int userId, int productId);
        void RemoveOrder(int orderId);
        void EditOrder(OrderModel model);
        List<OrderModel> GetOrders();
        List<OrderModel> GetOrdersByUser(int id);
    }
}

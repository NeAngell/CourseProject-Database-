using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;
using Курсовой_проект.Models;

namespace Курсовой_проект.Controllers
{
    [Route("api/[controller]")]
    public class ApiOrdersController : Controller
    {
        IAuthService authService;
        IOrderService orderService;

        public ApiOrdersController(IAuthService authService, IOrderService orderService) {
            this.authService = authService;
            this.orderService = orderService;
        }

        [HttpPost("add")]
        public void AddOrder([FromBody] int id) {
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.IsAuthorized(Request.Cookies["session"])) {
                    if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "U") {
                        orderService.AddOrder( authService.GetUser(Request.Cookies["session"]).UserId, id);
                    }
                }
            }
        }

        [HttpGet]
        public List<OrderModel> GetOrders() {
            List<OrderModel> orders = new List<OrderModel>();
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                    {
                        orders = orderService.GetOrders();
                    }
                }
            }
            return orders;
        }

        [HttpGet("myOrders")]
        public List<OrderModel> GetMyOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "U")
                    {
                        orders = orderService.GetOrdersByUser(authService.GetUser(Request.Cookies["session"]).UserId);
                    }
                }
            }
            return orders;
        }

        [HttpPost("remove")]
        public void RemoveOrder([FromBody] int id) {
            orderService.RemoveOrder(id);
        }

        [HttpPost("edit")]
        public void EditOrder([FromBody] OrderModel model) {
            orderService.EditOrder(model);
        }
    }
}

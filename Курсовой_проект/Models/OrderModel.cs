using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public UserModel User { get; set; }
        public SimpleCarModel Car { get; set; }
        public DateTime Date { get; set; }
        public bool DeliveryStatus { get; set; }
        public bool PaymentState { get; set; }

        public OrderModel(int orderId, UserModel user, SimpleCarModel car, DateTime date, bool deliveryStatus, bool paymentState) {
            OrderId = orderId;
            User = user;
            Car = car;
            Date = date;
            DeliveryStatus = deliveryStatus;
            PaymentState = paymentState;
        }
    }
}

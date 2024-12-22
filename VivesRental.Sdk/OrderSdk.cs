using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderSdk
    {
        private readonly IOrderService _orderService;

        public OrderSdk(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<OrderResult?> Get(Guid id)
        {
            return _orderService.Get(id);
        }

        public Task<List<OrderResult>> Find(OrderFilter? filter)
        {
            return _orderService.Find(filter);
        }

        public Task<OrderResult?> Create(Guid customerId)
        {
            return _orderService.Create(customerId);
        }

        public Task<bool> Return(Guid id, DateTime returnedAt)
        {
            return _orderService.Return(id, returnedAt);
        }
    }
}

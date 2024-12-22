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
    public class OrderLineSdk
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineSdk(IOrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        public Task<OrderLineResult?> Get(Guid id)
        {
            return _orderLineService.Get(id);
        }

        public Task<List<OrderLineResult>> Find(OrderLineFilter? filter)
        {
            return _orderLineService.Find(filter);
        }

        public Task<bool> Rent(Guid orderId, Guid articleId)
        {
            return _orderLineService.Rent(orderId, articleId);
        }

        public Task<bool> Rent(Guid orderId, IList<Guid> articleIds)
        {
            return _orderLineService.Rent(orderId, articleIds);
        }

        public Task<bool> Return(Guid orderLineId, DateTime returnedAt)
        {
            return _orderLineService.Return(orderLineId, returnedAt);
        }
    }
}

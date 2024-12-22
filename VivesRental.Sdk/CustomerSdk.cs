using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class CustomerSdk
    {
        private readonly ICustomerService _customerService;

        public CustomerSdk(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Task<CustomerResult?> Get(Guid id)
        {
            return _customerService.Get(id);
        }

        public Task<List<CustomerResult>> Find(CustomerFilter? filter)
        {
            return _customerService.Find(filter);
        }

        public Task<CustomerResult?> Create(CustomerRequest entity)
        {
            return _customerService.Create(entity);
        }

        public Task<CustomerResult?> Edit(Guid id, CustomerRequest entity)
        {
            return _customerService.Edit(id, entity);
        }

        public Task<bool> Remove(Guid id)
        {
            return _customerService.Remove(id);
        }
    }
}

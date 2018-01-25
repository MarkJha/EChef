using eCafe.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using eCafe.Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Core.Entities;
using System.Linq;
using eCafe.Infrastructure.Mapper;

namespace eCafe.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto model)
        {
            var entity = await _customerRepository
                         .InsertAsync(model.ToEntity());

            return entity.ToViewModel();
        }

        public async Task<CustomerDto> DeleteCustomerAsync(int id)
        {
            var entity = await _customerRepository
                       .DeleteAsync(new Customer { Id = id });

            return entity.ToViewModel();
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomerAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var model = await _customerRepository
                            .AllIncludingAsync(c => c.Orders);

            return model
                        .OrderBy(a => a.Id)
                        .Skip(pageNumber - 1 * pageSize)
                        .Take(pageSize)
                        .Select(item => item.ToViewModel())
                        .ToList();
        }

        public async Task<CustomerDto> GetCustomerAsync(int id)
        {
            var entity = await _customerRepository
                              .GetAsync(new Customer { Id = id });

            return entity.ToViewModel();
        }

        public Task<IEnumerable<CustomerDto>> GetCustomerAsyncV2(ResourceParameter parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCustomerDuplicateAsync(CustomerDto model) 
            => await _customerRepository.IsExistsAsync(x => x.Name.Equals(model.Name));

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto model)
        {
            var entity = await _customerRepository
                           .UpdateAsync(model.ToEntity());

            return entity != null ? entity.ToViewModel() : new CustomerDto();
        }

        public Task<CustomerDto> UpdateCustomerPartiallyAsync(int id, JsonPatchDocument<CustomerDto> CustomerPatch)
        {
            throw new NotImplementedException();
        }
    }
}

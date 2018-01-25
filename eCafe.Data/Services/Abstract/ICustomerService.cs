using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomerAsync(int pageSize = 10, int pageNumber = 1, string name = null);

        Task<IEnumerable<CustomerDto>> GetCustomerAsyncV2(ResourceParameter parameters);

        Task<CustomerDto> GetCustomerAsync(int id);

        Task<CustomerDto> CreateCustomerAsync(CustomerDto model);

        Task<CustomerDto> UpdateCustomerAsync(CustomerDto model);

        Task<CustomerDto> UpdateCustomerPartiallyAsync(int id, JsonPatchDocument<CustomerDto> CustomerPatch);

        Task<CustomerDto> DeleteCustomerAsync(int id);

        Task<bool> IsCustomerDuplicateAsync(CustomerDto model);
    }
}

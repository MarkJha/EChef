using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Repository
{
    public class CustomerRepository: EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ECafeContext context)
            : base(context)
        {

        }
    }
}

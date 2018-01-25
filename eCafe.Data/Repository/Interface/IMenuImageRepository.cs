using eCafe.Core.Entities;
using eCafe.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository.Interface
{
    public interface IMenuImageRepository : IEntityBaseRepository<SubMenuImageDetail>
    {
         Task<int> DeleteMenuImageDetailAsync(int MenuId);
    }
}

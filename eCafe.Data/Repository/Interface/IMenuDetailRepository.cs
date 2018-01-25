using eCafe.Core.Entities;
using eCafe.Core.Repository;
using eCafe.Infrastructure.Mapper;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository.Interface
{
    public interface IMenuDetailRepository : IEntityBaseRepository<MenuDetail>
    {
        void UploadPicture();
        Task<IEnumerable<MenuDetailDto>> GetAllMenusBySearchKey(string searchKey);
    }
}

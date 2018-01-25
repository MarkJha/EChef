using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository
{
    public class MenuImageRepository : EntityBaseRepository<SubMenuImageDetail>, IMenuImageRepository
    {
        private ECafeContext _context;
        public MenuImageRepository(ECafeContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<int> DeleteMenuImageDetailAsync(int MenuId)
        {
            var imageDetailDeletedObject = _context.SubMenuImageDetails.Where(i => i.MenuDetailId.Equals(MenuId));
            if (imageDetailDeletedObject != null)
            {
                _context.SubMenuImageDetails.RemoveRange(imageDetailDeletedObject);

                var subMenuDetailDeletedObject = _context.MenuDetails
                                                  .FirstOrDefault(m => m.Id.Equals(MenuId));
                if (subMenuDetailDeletedObject != null)
                {
                    _context.MenuDetails.Remove(subMenuDetailDeletedObject);
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}

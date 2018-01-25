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
    public class MenuRepository : EntityBaseRepository<Menu>, IMenuRepository
    {
        private ECafeContext _context;
        public MenuRepository(ECafeContext context)
            : base(context)
        {
            _context = context;
        }

        public void SetPicture(int id, string root, string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteMenuAsync(int MenuId)
        {
            //*** First Get All Menu Details for particular selected menu
            var subMenuDetailDeletedObject = _context.MenuDetails
                                                 .Where(m => m.MenuId.Equals(MenuId));
            
            if (subMenuDetailDeletedObject!=null)
            {
                //*** Delete all image details for menu details
                foreach (var item in subMenuDetailDeletedObject)
                {
                    var imageDetailDeletedObject = _context.SubMenuImageDetails.Where(i => i.MenuDetailId.Equals(item.Id));
                    if (imageDetailDeletedObject != null)
                    {
                        _context.SubMenuImageDetails.RemoveRange(imageDetailDeletedObject);
                    }
                }
                //*** removing all menu details 
                _context.MenuDetails.RemoveRange(subMenuDetailDeletedObject);
            }
            //*** remove menu from DB
            var menuDeletedObject = _context.Menus.FirstOrDefault(m => m.Id.Equals(MenuId));
            if (menuDeletedObject != null)
            {
                _context.Menus.Remove(menuDeletedObject);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}

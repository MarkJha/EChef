using eCafe.Core.Entities;
using eCafe.Core.Repository;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository.Interface
{
    public interface IMenuRepository : IEntityBaseRepository<Menu>
    {
        void SetPicture(int id, string root, string filename);
        Task<int> DeleteMenuAsync(int MenuId);
    }


}

using eCafe.Core.Entities;
using eCafe.Core.Repository;

namespace eCafe.Infrastructure.Repository.Interface
{
    public interface IMainMenuRepository : IEntityBaseRepository<MainMenu>
    {
        void BatchDelete(int[] ids);
    }


}

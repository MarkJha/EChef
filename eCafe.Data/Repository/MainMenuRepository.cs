using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Repository.Interface;
using System.Collections.Generic;

namespace eCafe.Infrastructure
{
    public class MainMenuRepository : EntityBaseRepository<MainMenu>, IMainMenuRepository
    {
        public MainMenuRepository(ECafeContext context)
            : base(context)
        {

        }        

        public void BatchDelete(int[] ids)
        {
            throw new System.NotImplementedException();
        }
    }
}

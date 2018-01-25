using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface IMainMenuService
    {
        Task<IEnumerable<MainMenuDto>> GetAsync(int pageSize = 10, int pageNumber = 1, string name = null);
        IEnumerable<SelectOption> GetSelectOption();
    }
}

using System.Collections.Generic;
using eCafe.Infrastructure.Models;
using System.Linq;
using eCafe.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Repository.Interface;

namespace eCafe.Infrastructure.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IMainMenuRepository _mainMenuRepository;

        public MainMenuService(IMainMenuRepository mainMenuRepository)
        {
            _mainMenuRepository = mainMenuRepository;
        }

        public async Task<IEnumerable<MainMenuDto>> GetAsync(int pageSize = 10, int pageNumber = 1, string name = null)
                            => await _mainMenuRepository
                            .GetAllAsync(pageSize, pageNumber, name)
                            .Select(item => item.ToViewModel())
                            .OrderByDescending(x => x.Id)
                            .ToListAsync();


        public IEnumerable<SelectOption> GetSelectOption()
                           => _mainMenuRepository
                              .GetAll()
                              .OrderBy(x => x.Name)
                              .Select(menu => new SelectOption
                              {
                                  Value = menu.Name,
                                  Id = menu.Id
                              }).ToList();
    }
}

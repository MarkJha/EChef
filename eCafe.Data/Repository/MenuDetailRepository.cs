using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Extensions;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using eCafe.Infrastructure.Mapper;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository
{
    public class MenuDetailRepository : EntityBaseRepository<MenuDetail>, IMenuDetailRepository
    {
        /// <summary>
        /// constants for repository class
        /// </summary>
        private const string SpForGetMenus = "spMenuNameSearch";
        private const string ExecuteCommand = "exec";
        private const string ParametersForGetMenus = "@searchTearm";

        private ECafeContext _context;
        public MenuDetailRepository(ECafeContext context)
            : base(context)
        {
            _context = context;
        }

        public void UploadPicture()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MenuDetailDto>> GetAllMenusBySearchKey(string searchKey)
        {
            return await GetAllByMenuName(searchKey);
        }

        private async Task<List<MenuDetailDto>> GetAllByMenuName(string menuName)
        {
            var cleanMenuName = menuName.RemoveSpecialCharcters();

            // execute stored procedure and return program list from database
            var menuDetailList = await CallSpForGetMenus(menuName);
            foreach (var item in menuDetailList)
            {
                // calculate string similarity and rounded up-to 2 decimals
                item.MatchPercent = Math.Round(CalculateSimilarity(item.Name, cleanMenuName), 2);
            }
            return menuDetailList
                    .OrderByDescending(x => x.MatchPercent)
                    .ToList();
        }

        /// <summary>
        /// Calculate similarity between two input strings
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns> percentage of similarity of two strings </returns>
        private double CalculateSimilarity(string str1, string str2)
        {
            return str1.ToLower().Similarity(str2.ToLower()) * 100;
        }

        private async Task<IEnumerable<MenuDetailDto>> CallSpForGetMenus(string searchText)
        {
            var menusList = new List<MenuDetailDto>();
            using (var context = _context)
            {
                var menuName = new SqlParameter(ParametersForGetMenus, searchText);
                menusList = await context.MenuDetails
                             .FromSql(CreateSpCommand(SpForGetMenus, ParametersForGetMenus), menuName)
                             .AsNoTracking()
                             .Select(item => item.ToViewModel())
                             .ToListAsync();
            }
            return menusList;
        }

        /// <summary>
        /// create stored procedure execution command
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spParameters"></param>
        /// <returns></returns>
        private string CreateSpCommand(string spName, string spParameters)
        {
            return $"{spName} {spParameters}";
        }
    }

}

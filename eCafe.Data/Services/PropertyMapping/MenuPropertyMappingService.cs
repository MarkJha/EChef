using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace eCafe.Infrastructure.Services.PropertyMapping
{
    public class MenuPropertyMappingService : BasePropertyMappingService, IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _mainMenuPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "MainMenuId", new PropertyMappingValue(new List<string>() { "MainMenuId" } ) },
               { "Name", new PropertyMappingValue(new List<string>() { "Name" } )},
               { "Description", new PropertyMappingValue(new List<string>() { "Description" } , true) },
               { "ImagePath", new PropertyMappingValue(new List<string>() { "ImagePath" } , true) }
              // { "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
           };


        public MenuPropertyMappingService()
        {
            PropertyMappings.Add(new PropertyMapping<MenuDto, Menu>(_mainMenuPropertyMapping));
        }
    }
}

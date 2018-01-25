using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace eCafe.Infrastructure.Services.PropertyMapping
{
    public class MainMenuPropertyMappingService : BasePropertyMappingService, IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _mainMenuPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "Name", new PropertyMappingValue(new List<string>() { "Name" } )},
               { "Description", new PropertyMappingValue(new List<string>() { "Description" } , true) }
              // { "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
           };


        public MainMenuPropertyMappingService()
        {
            PropertyMappings.Add(new PropertyMapping<MainMenuDto, MainMenu>(_mainMenuPropertyMapping));
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Extensions
{
    /// <summary>
    /// This class is for debugging ModelState errors either in the quick watch 
    /// window or the immediate window.
    /// When the model state contains dozens and dozens of properties, 
    /// it is impossible to inspect why a model state is invalid.
    /// This method will pull up the errors
    /// </summary>
    public static class ModelStateErrorHandler
    {
        /// <summary>
        /// Returns a Key/Value pair with all the errors in the model
        /// according to the data annotation properties.
        /// </summary>
        /// <param name="errDictionary"></param>
        /// <returns>
        /// Key: Name of the property
        /// Value: The error message returned from data annotation
        /// </returns>
        public static Dictionary<string, string> GetModelErrors(this ModelStateDictionary errDictionary)
        {
            var errors = new Dictionary<string, string>();
            errDictionary.Where(k => k.Value.Errors.Count > 0).ToList().ForEach(i =>
            {
                var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                errors.Add(i.Key, er);
            });
            return errors;
        }

        public static string StringifyModelErrors(this ModelStateDictionary errDictionary)
        {
            var errorsBuilder = new StringBuilder();
            var errors = errDictionary.GetModelErrors();
            errors.ToList().ForEach(key => errorsBuilder.AppendFormat("{0}: {1} -", key.Key, key.Value));
            return errorsBuilder.ToString();
        }
    }
}

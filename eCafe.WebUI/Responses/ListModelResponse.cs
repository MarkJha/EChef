using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.WebUI.Responses
{
    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public string Message { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public IEnumerable<ExpandoObject> ShapeModel { get; set; }

      
    }
}

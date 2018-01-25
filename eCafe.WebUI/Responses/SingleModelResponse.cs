using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.WebUI.Responses
{
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel>
    {
        public string Message { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}

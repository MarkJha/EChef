using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.WebUI.Responses
{
    public interface IResponse
    {
        string Message { get; set; }

        bool IsError { get; set; }

        string ErrorMessage { get; set; }
    }
}

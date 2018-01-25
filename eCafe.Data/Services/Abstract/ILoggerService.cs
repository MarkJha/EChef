using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Services.Abstract
{
   public interface ILoggerService
    {
        void LogError(Error error);
    }
}

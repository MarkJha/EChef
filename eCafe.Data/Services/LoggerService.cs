using eCafe.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using eCafe.Core.Entities;
using eCafe.Infrastructure.Repository.Interface;

namespace eCafe.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _loggerRepository;

        public LoggerService(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public void LogError(Error error)
        {
            _loggerRepository.Add(error);
            _loggerRepository.Commit();
        }
    }
}

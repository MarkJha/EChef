using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Repository
{
    public class LoggerRepository : EntityBaseRepository<Error>, ILoggerRepository
    {
        public LoggerRepository(ECafeContext context)
            : base(context)
        {

        }

        //public override void Commit()
        //{
        //    try
        //    {
        //        base.Commit();
        //    }
        //    catch { }
        //}

    }
}

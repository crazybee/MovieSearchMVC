using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApi.UnitOfWork
{
    public enum UnitOfWorkStatus
    {
        Ok,
        NotFound,
        Conflict,
        Exception,
        Invalid,
        Forbidden,
        BadRequest
    }
}
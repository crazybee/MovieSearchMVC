using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApi.UnitOfWork
{
    internal interface IUnitOfWork
    {
        UnitOfWorkResult Execute();

    }
}
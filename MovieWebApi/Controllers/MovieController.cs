using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using MovieWebApi.UnitOfWork;
using MovieWebApi.UnitOfWork.Workers;
using WebApi.OutputCache.V2;

namespace MovieWebApi.Controllers
{
    public class MovieController : ApiController
    {

        /// <summary>
        /// we are using autofac to handle dependency injection
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;
        public MovieController(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// we use output cache to cache the response, here we specify the cache timeout for 100 sec
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult SearhByName(string movieName)
        {
            try
            {
                var uow = _lifetimeScope.Resolve<SearchMovieByName>(new NamedParameter("movieName", movieName));
                var uowResult = uow.Execute();
                switch (uowResult.Status)
                {
                    case UnitOfWorkStatus.Ok:
                        return Ok(uowResult.Result);
                    case UnitOfWorkStatus.NotFound:
                        return new HttpActionResult(HttpStatusCode.NotFound, uowResult.Result.ToString());
                    default:
                        var innerEx = uowResult.Exception.InnerException;
                        return new HttpActionResult(HttpStatusCode.InternalServerError, string.IsNullOrEmpty(innerEx?.Message) ? uowResult.Exception.Message : innerEx.Message);

                }
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using MovieWebApi.UnitOfWork;
using MovieWebApi.UnitOfWork.Workers;
using Module = Autofac.Module;

namespace MovieWebApi.Configuration
{
    public class ModuleRegister : Module
    {
        /// <summary>
        /// for use in Autofac
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SearchMovieByName>().AsSelf().InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.IsAssignableTo<IUnitOfWork>()).AsSelf()
                .WithParameter(
                    (pi, c) => pi.ParameterType == typeof(IMapper),
                    (pi, c) => c.Resolve<IMapper>())
                .InstancePerRequest();
        }
    }
}
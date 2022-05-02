using Autofac;
using HDV.Nhom2.Domain;
using HDV.Nhom2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi
{
    /// <summary>
    /// Module DI
    /// </summary>
    /// CreatedBy: dbhuan 02/05/2022
    public class DIModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommonUtility>()
                .As<ICommonUtility>();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("HDV.Nhom2.Infrastructure"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("HDV.Nhom2.Application"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}

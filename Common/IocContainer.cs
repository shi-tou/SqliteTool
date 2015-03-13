using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Configuration;

namespace Common
{
    /// <summary>
    /// autofac注入 容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class IocContainer<T> where T:class
    {
        private static ContainerBuilder builder;
        public static T GetInstance()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<T>();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            using (var container = builder.Build())
            {
                return container.Resolve<T>();
            }
        }
    }
}

using Assessment.Stock.Business.DependencyResolvers.AutoMapper;
using Autofac;
using AutoMapper;

namespace Assessment.Stock.Business.DependencyResolvers.Autofac
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                // Register mapper profile
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<LogProfile>();
                cfg.AddProfile<AuthenticationProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                // register IMapper component as scope
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}

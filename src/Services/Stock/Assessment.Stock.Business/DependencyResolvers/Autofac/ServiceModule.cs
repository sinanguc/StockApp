using Assessment.Common.Security.Token;
using Assessment.Stock.Business.Abstract.AuthenticationService;
using Assessment.Stock.Business.Abstract.Log;
using Assessment.Stock.Business.Abstract.OrderService;
using Assessment.Stock.Business.Abstract.UserService;
using Assessment.Stock.Business.Concrete.AuthenticationManager;
using Assessment.Stock.Business.Concrete.Log;
using Assessment.Stock.Business.Concrete.OrderManager;
using Assessment.Stock.Business.Concrete.UserManager;
using Autofac;

namespace Assessment.Stock.Business.DependencyResolvers.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationManager>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenManager>().As<ITokenService>().InstancePerLifetimeScope();
            builder.RegisterType<LogManager>().As<ILogService>().InstancePerLifetimeScope();
        }
    }
}

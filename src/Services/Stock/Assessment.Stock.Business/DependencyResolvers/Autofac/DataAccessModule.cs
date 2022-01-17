using Autofac;
using Assessment.Stock.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Assessment.Stock.DataAccess.Concrete;
using Assessment.Stock.DataAccess.Abstract;
using Assessment.Stock.Core.Utilities.Configuration;
using Assessment.Enum.Common;
using Assessment.Common.Infrastructure.ErrorHandling;
using Assessment.Common.Infrastructure.Mongo;

namespace Assessment.Stock.Business.DependencyResolvers.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var provider = StockAppConfiguration.BaseConfig.GetDatabaseProvider();
            var connectionString = StockAppConfiguration.BaseConfig.GetConnectionString();
            DbContextOptionsBuilder dbContextOptionsBuilder;
            switch (provider)
            {
                case nameof(EnumProvider.PostgreSQL):
                    dbContextOptionsBuilder = new DbContextOptionsBuilder<StockContext>().UseNpgsql(connectionString,
                        options => options.MigrationsAssembly("Assessment.Stock.DataAccess"));
                    break;
                default:
                    throw new DatabaseException(Enum.Stock.Messages.StockMessages.DatabaseProviderBulunamadi);
            }

            builder.RegisterType<StockContext>()
                .WithParameter("options", dbContextOptionsBuilder.Options).InstancePerLifetimeScope();

            builder.RegisterType<MongoContext>().As<IMongoContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }        
    }
}

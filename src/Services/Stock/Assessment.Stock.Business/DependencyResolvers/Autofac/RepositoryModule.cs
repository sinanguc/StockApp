using Assessment.Stock.DataAccess.Abstract.Repositories;
using Assessment.Stock.DataAccess.Concrete.EntityFramework.Repositories;
using Autofac;

namespace Assessment.Stock.Business.DependencyResolvers.Autofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfEntityRepositoryBase<>)).As(typeof(IEntityRepository<>)).InstancePerLifetimeScope();
        }
    }
}

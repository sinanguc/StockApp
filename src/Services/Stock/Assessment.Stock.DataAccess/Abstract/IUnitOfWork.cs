using Assessment.Stock.DataAccess.Abstract.Repositories;
using Assessment.Stock.DataAccess.Concrete.EntityFramework.Contexts;
using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Users;
using System.Threading.Tasks;

namespace Assessment.Stock.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        public StockContext StockContext { get; }

        public IEntityRepository<User> UserRepository { get; }
        public IEntityRepository<Order> OrderRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

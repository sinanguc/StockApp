using Assessment.Common.Infrastructure.ErrorHandling;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.DataAccess.Abstract;
using Assessment.Stock.DataAccess.Abstract.Repositories;
using Assessment.Stock.DataAccess.Concrete.EntityFramework.Contexts;
using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Users;
using System;
using System.Threading.Tasks;

namespace Assessment.Stock.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public StockContext StockContext { get; private set; }

        public IEntityRepository<User> UserRepository { get; private set; }

        public IEntityRepository<Order> OrderRepository { get; private set; }

        public UnitOfWork(StockContext stockContext, 
            IEntityRepository<User> userRepository, 
            IEntityRepository<Order> orderRepository)
        {
            StockContext = stockContext;
            UserRepository = userRepository;
            OrderRepository = orderRepository;
        }

        public void SaveChanges()
        {
            int count = StockContext.SaveChanges();
            if (count <= 0) // Veri tabanına kayıt eklenirken, hiç bir kayıt etkilenmediyse hata döndür
                throw new DatabaseException(StockMessages.DatabaseKaydiSirasindaHataOlustu);
        }

        public async Task SaveChangesAsync()
        {
            int count = await StockContext.SaveChangesAsync();
            if (count <= 0) // Veri tabanına kayıt eklenirken, hiç bir kayıt etkilenmediyse hata döndür
                throw new DatabaseException(StockMessages.DatabaseKaydiSirasindaHataOlustu);
        }

        #region Disposable
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                StockContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

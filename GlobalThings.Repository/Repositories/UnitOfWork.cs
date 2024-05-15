
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Repository.Context;
using MongoDB.Driver;

namespace GlobalThings.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public ISensorRepository SensorRepository { get; }
        public IEquipamentRepository EquipamentRepository { get; }
        public IMeasurementRepository MeasurementRepository { get; }

        public async Task AbortTransaction(IClientSessionHandle session)
        {
            await session.CommitTransactionAsync();
        }

        public async Task CommitTransaction(IClientSessionHandle session)
        {
            await session.CommitTransactionAsync();
        }
        public async Task<IClientSessionHandle> StartTransaction()
        {
            IClientSessionHandle session = await _context.Cliente.StartSessionAsync();
            session.StartTransaction();
            return session;
        }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            SensorRepository = new SensorRepository(_context);
            EquipamentRepository = new EquipamentRepository(_context);
            MeasurementRepository = new MeasurementRepository(_context);
        }
    }
}

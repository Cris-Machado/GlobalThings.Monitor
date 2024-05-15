using MongoDB.Driver;

namespace GlobalThings.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public ISensorRepository SensorRepository { get; }
        public IEquipamentRepository EquipamentRepository { get; }
        public IMeasurementRepository MeasurementRepository { get; }
        Task<IClientSessionHandle> StartTransaction();
        Task CommitTransaction(IClientSessionHandle session);
        Task AbortTransaction(IClientSessionHandle session);
    }
}

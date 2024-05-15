using GlobalThings.Domain.Entities;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Models;
using GlobalThings.Repository.Context;
using GlobalThings.Repository.Repositories.Base;

namespace GlobalThings.Repository.Repositories
{
    public class SensorRepository : RepositoryBase<SensorModel, SensorEntity>, ISensorRepository
    {
        public SensorRepository(DbContext context) : base(context)
        {
        }

        protected override string Collection => "sensors";
    }
}

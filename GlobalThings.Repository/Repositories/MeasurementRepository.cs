using GlobalThings.Domain.Entities;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Models;
using GlobalThings.Repository.Context;
using GlobalThings.Repository.Repositories.Base;

namespace GlobalThings.Repository.Repositories
{
    public class MeasurementRepository : RepositoryBase<MeasurementModel, MeasurementEntity>, IMeasurementRepository
    {
        public MeasurementRepository(DbContext context) : base(context)
        {
        }

        protected override string Collection => "measurements";
    }
}

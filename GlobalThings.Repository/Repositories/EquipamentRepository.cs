using GlobalThings.Domain.Entities;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Models;
using GlobalThings.Repository.Context;
using GlobalThings.Repository.Repositories.Base;

namespace GlobalThings.Repository.Repositories
{
    public class EquipamentRepository : RepositoryBase<EquipamentModel, EquipamentEntity>, IEquipamentRepository
    {
        public EquipamentRepository(DbContext context) : base(context)
        {
        }

        protected override string Collection => "equipaments";
    }
}

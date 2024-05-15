using System.Linq.Expressions;

namespace GlobalThings.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> ListAllActive();
        Task<T> FindById(string id);
        Task<T> FindByDescription(string id);
        Task<T> Where(Expression<Func<T, bool>> where);
        Task<List<T>> WhereList(Expression<Func<T, bool>> where);
        Task<string> Add(T model, string modelId);
        Task Update(T model, string id);
        Task Delete(string id);
    }

}

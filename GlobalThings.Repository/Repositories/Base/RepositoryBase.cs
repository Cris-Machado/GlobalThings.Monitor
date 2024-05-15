using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;

namespace GlobalThings.Repository.Repositories.Base
{
    public abstract class RepositoryBase<TModel, TEntity> : IRepositoryBase<TModel>
       where TModel : class
       where TEntity : class
    {
        protected virtual string Collection => typeof(TEntity).Name.ToLower();
        protected IMongoCollection<TEntity> _collection;
        protected readonly Context.DbContext _context;
        protected readonly IMapper _mapper;

        public RepositoryBase(Context.DbContext context)
        {
            _context = context;
            _collection = _context.MongoDb.GetCollection<TEntity>(Collection);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile(typeof(MappingProfile));
            });

            _mapper = config.CreateMapper();
        }
        public async Task<TModel> Where(Expression<Func<TModel, bool>> where)
        {

            var whereExp = _mapper.Map<Expression<Func<TEntity, bool>>>(where);
            var filtered = _collection.Find(whereExp);

            TEntity entity = await filtered.FirstOrDefaultAsync();
            return _mapper.Map<TModel>(entity);
        }
        public async Task<List<TModel>> WhereList(Expression<Func<TModel, bool>> where)
        {

            var whereExp = _mapper.Map<Expression<Func<TEntity, bool>>>(where);
            var filtered = _collection.Find(whereExp);

            List<TEntity> entityList = await filtered.ToListAsync();
            return _mapper.Map<List<TModel>>(entityList);
        }
        public async Task<List<TModel>> ListAllActive()
        {
            List<TEntity> entityList = await _collection.Find(Builders<TEntity>.Filter.Eq("Active", true)).ToListAsync();
            return _mapper.Map<List<TModel>>(entityList);
        }
        public async Task<TModel> FindById(string id)
        {
            TEntity entityList = await _collection.Find(Builders<TEntity>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();
            return _mapper.Map<TModel>(entityList);
        }
        public async Task<TModel> FindByDescription(string description)
        {
            TEntity entityList = await _collection.Find(Builders<TEntity>.Filter.Eq("Description", description)).FirstOrDefaultAsync();
            return _mapper.Map<TModel>(entityList);
        }
        public async Task<string> Add(TModel model, string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
            {
                modelId = ObjectId.GenerateNewId().ToString();
                PropertyInfo id = typeof(TModel).GetProperties().FirstOrDefault(e => e.Name == "Id");
                id.SetValue(model, modelId);
            }

            TEntity entity = _mapper.Map<TEntity>(model);
            await _collection.InsertOneAsync(entity);

            return modelId;
        }
        public async Task Update(TModel model, string id)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", new ObjectId(id)), entity);
        }
        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", new ObjectId(id)));
        }
    }
}

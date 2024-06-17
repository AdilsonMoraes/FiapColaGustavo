using FiapSmartCityDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapSmartCityInfrastructure.GenericInterfaces
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SmartCitiesContext _contextSql;
        private DbSet<TEntity> entities;

        public GenericRepository(SmartCitiesContext contextSql)
        {
            _contextSql = contextSql;
            entities = contextSql.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entities.AsEnumerable().ToList();
        }
        public async Task<TEntity> GetById(long id)
        {
            return await entities.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await _contextSql.SaveChangesAsync();
        }
        public async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            await _contextSql.SaveChangesAsync();
        }
        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await _contextSql.SaveChangesAsync();
        }

    }
}

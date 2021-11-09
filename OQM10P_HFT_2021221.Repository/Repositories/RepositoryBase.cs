using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Linq;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepoBase<TEntity, TKey> where TEntity : class
    {

        protected IssueManagementDbContext Context;

        protected RepositoryBase(IssueManagementDbContext context)
        {
            Context = context;
        }

        public TEntity Create(TEntity entity)
        {
            var result = Context.Add(entity);
            Context.SaveChanges();
            return result.Entity;
        }

        public void Delete(TKey key)
        {
            Context.Remove(Read(key));
        }

        //public TEntity Read(TKey key) {
        //    return Context.Find<TEntity>(key);
        //}

        public abstract TEntity Read(TKey key);

        public IQueryable<TEntity> ReadAll()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            var result = Context.Update(entity);
            Context.SaveChanges();
            return result.Entity;
        }
    }
}

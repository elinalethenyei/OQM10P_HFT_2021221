using OQM10P_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public abstract class RepoBase<TEntity, TKey> : IRepoBase<TEntity, TKey> where TEntity : class
    {

        protected IssueManagementDbContext Context;

        protected RepoBase(IssueManagementDbContext context)
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

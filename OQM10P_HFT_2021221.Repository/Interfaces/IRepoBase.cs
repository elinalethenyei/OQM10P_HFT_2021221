using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Repository.Interfaces
{
    public interface IRepoBase<TEntity,TKey> where TEntity : class
    {
        IQueryable<TEntity> ReadAll();

        TEntity Read(TKey key);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TKey key);
    }
}

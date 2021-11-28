using System.Collections.Generic;

namespace OQM10P_HFT_2021221.Logic.Interfaces
{
    public interface IBaseService<TEntity, TKey>
    {
        IList<TEntity> ReadAll();

        TEntity Read(TKey id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TKey id);
    }
}

using System.Collections.Generic;

namespace Asteroids.Infrastructure.ViewModel
{
    public interface IViewModel<TEntity, TEntityView>
    {
        IDictionary<TEntity, TEntityView> ViewsByEntity { get; }

        void AddView(TEntity entity, TEntityView entityView);
        void RemoveView(TEntity entity);

        TEntityView GetView(TEntity entity);

        void Clear();
    }
}
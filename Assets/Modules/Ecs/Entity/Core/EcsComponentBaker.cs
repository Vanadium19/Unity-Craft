using UnityEngine;

namespace Leopotam.EcsLite
{
    public interface IEcsComponentBaker
    {
        void Bake(EcsWorld world, int entity);
    }

    public abstract class EcsComponentBaker<T> : MonoBehaviour, IEcsComponentBaker where T : struct
    {
        public void Bake(EcsWorld world, int entity)
        {
            EcsPool<T> pool = world.GetPool<T>();

            if (pool.Has(entity)) pool.Get(entity) = this.Bake();
            else pool.Get(entity) = this.Bake();
        }

        protected abstract T Bake();
    }
}
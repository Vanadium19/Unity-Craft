using UnityEngine;

namespace Leopotam.EcsLite
{
    public abstract class EcsComponentInstaller<T> : MonoBehaviour, IEcsEntityInstaller where T : struct
    {
        public void Install(EcsWorld world, int entity)
        {
            EcsPool<T> pool = world.GetPool<T>();

            T value = this.GetValue();
            
            if (pool.Has(entity))
                pool.Get(entity) = value;
            else
                pool.Add(entity) = value;
        }

        protected abstract T GetValue();
    }
}
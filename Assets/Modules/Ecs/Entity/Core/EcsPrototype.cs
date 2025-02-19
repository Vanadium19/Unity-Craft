using UnityEngine;

namespace Leopotam.EcsLite
{
    public abstract class EcsPrototype : ScriptableObject
    {
        public virtual string Name => this.name;

        public int Create(in EcsWorld world)
        {
            int entity = world.NewEntity();
            world.GetPool<EcsName>().Add(entity).value = this.Name;
            this.Install(in world, in entity);
            return entity;
        }

        protected abstract void Install(in EcsWorld world, in int entity);
    }
}
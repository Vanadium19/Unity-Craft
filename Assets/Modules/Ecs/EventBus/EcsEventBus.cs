using System;
using System.Collections.Generic;

namespace Leopotam.EcsLite
{
    public sealed class EcsEventBus
    {
        private readonly Dictionary<Type, IEcsQueue> _events = new();

        public EcsStack<T> GetStack<T>() where T : struct
        {
            Type eventType = typeof(T);
            
            if (_events.TryGetValue(eventType, out IEcsQueue iPool))
                return (EcsStack<T>) iPool;

            EcsStack<T> pool = new EcsStack<T>();
            _events.Add(eventType, pool);
            return pool;
        }
    }
}
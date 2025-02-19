using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public struct EcsEventInject<T> : IEcsCustomDataInject where T : struct
    {
        public EcsStack<T> Value;

        void IEcsCustomDataInject.Fill(object[] injects)
        {
            foreach (object inject in injects)
                if (inject is EcsEventBus eventBus)
                    this.Value = eventBus.GetStack<T>();

            throw new Exception("Ecs Event Bus not injected!");
        }
    }
}
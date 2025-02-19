using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Context.Inputs
{
    public class InputSystem : IEcsRunSystem
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private readonly EcsSharedInject<GameData> _gameData;

        public void Run(IEcsSystems systems)
        {
            ref InputData inputData = ref _gameData.Value.InputData;

            inputData.MoveDirection.x = Input.GetAxisRaw(HorizontalAxis);
            inputData.MoveDirection.z = Input.GetAxisRaw(VerticalAxis);

            inputData.IsFire = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
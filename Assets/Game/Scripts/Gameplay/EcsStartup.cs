using System;
using Game.Context;
using Game.Context.Inputs;
using Game.Context.Player;
using Game.Entities.Core;
using Game.Entities.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private EcsWorldView _view;

        [SerializeField] private GameData _gameData;

        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world, _gameData);
            _systems
                //Input
                .Add(new InputSystem())
                .Add(new PlayerMoveController())

                //Game Logic
                .Add(new MoveSystem())
                
                //Rendering:
                .Add(new TransformViewSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject()
                .Init();
        }

        private void Start()
        {
            _view.Show(_world);
        }

        private void Update()
        {
            // process systems here.
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }

        [Button]
        private void CreateCharacter()
        {
            int player = _world.NewEntity();

            _world.GetPool<Position>().Add(player).Value = Vector3.zero;
            _world.GetPool<MoveDirection>().Add(player);
            _world.GetPool<MoveSpeed>().Add(player).Value = 3f;
            _world.GetPool<EcsName>().Add(player).value = "Player";
        }
    }
}
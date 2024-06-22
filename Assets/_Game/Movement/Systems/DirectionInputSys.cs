using Leopotam.EcsLite;
using Movement.Comps;
using UnityRef.Transform.Comp;
using UnityRef.Transform.Extras.Direction;

namespace Movement.Systems {
  public class DirectionInputSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<DirectionInput> _directionInputPool;
    private EcsPool<EcsTransform>   _displacementPool;
    private EcsFilter               _filter;

    private EcsPool<MoveDirection> _moveDirectionPool;
    private EcsWorld               _world;

    public void Init(IEcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world
               .Filter<MoveDirection>()
               .Inc<DirectionInput>()
               .Inc<EcsTransform>()
               .End();

      _moveDirectionPool  = _world.GetPool<MoveDirection>();
      _directionInputPool = _world.GetPool<DirectionInput>();
      _displacementPool   = _world.GetPool<EcsTransform>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref MoveDirection  moveDirection  = ref _moveDirectionPool.Get(e);
        ref DirectionInput directionInput = ref _directionInputPool.Get(e);
        ref EcsTransform   displacement   = ref _displacementPool.Get(e);

        moveDirection.value = displacement.GetDirection(directionInput.value);
      }
    }
  }
}
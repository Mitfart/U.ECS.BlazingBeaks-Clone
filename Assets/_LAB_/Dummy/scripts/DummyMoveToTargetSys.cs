using Leopotam.EcsLite;
using Movement.Comps;
using Unit.Comps;
using UnityRef.Transform.Comp;

namespace _TEST {
  public class DummyMoveToTargetSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<AimTarget>     _aimTargetPool;
    private EcsFilter              _dummyFilter;
    private EcsPool<EcsTransform>  _ecsTransformPool;
    private EcsPool<MoveDirection> _moveDirectionPool;
    private EcsWorld               _world;


    public void Init(IEcsSystems systems) {
      _world       = systems.GetWorld();
      _dummyFilter = _world.Filter<DummyTag>().Inc<AimTarget>().Inc<MoveDirection>().Inc<EcsTransform>().End();

      _aimTargetPool     = _world.GetPool<AimTarget>();
      _ecsTransformPool  = _world.GetPool<EcsTransform>();
      _moveDirectionPool = _world.GetPool<MoveDirection>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int dummyE in _dummyFilter) {
        ref AimTarget     aimTarget      = ref _aimTargetPool.Get(dummyE);
        ref EcsTransform  dummyTransform = ref _ecsTransformPool.Get(dummyE);
        ref MoveDirection dummyMoveDir   = ref _moveDirectionPool.Get(dummyE);

        dummyMoveDir.value = (aimTarget.position - dummyTransform.position).normalized;
      }
    }
  }
}
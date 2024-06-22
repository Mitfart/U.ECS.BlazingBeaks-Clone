using Leopotam.EcsLite;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Unit.Sys {
  public class TurnToAimTargetSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _filter;
    private EcsFilter             _playerFilter;
    private EcsPool<AimTarget>    _viewDirectionPool;
    private EcsWorld              _world;


    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<EcsTransform>().Inc<AimTarget>().End();

      _displacementPool  = _world.GetPool<EcsTransform>();
      _viewDirectionPool = _world.GetPool<AimTarget>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref EcsTransform disp      = ref _displacementPool.Get(e);
        ref AimTarget    aimTarget = ref _viewDirectionPool.Get(e);

        Vector3 scale = disp.scale;

        if (aimTarget.position.x < disp.position.x)
          scale.x = -Mathf.Abs(scale.x);
        else if (aimTarget.position.x > disp.position.x)
          scale.x = Mathf.Abs(scale.x);

        disp.scale = scale;
      }
    }
  }
}
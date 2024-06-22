using Extentions;
using Leopotam.EcsLite;
using Player;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace _TEST {
  public class DummyTargetPlayerSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<AimTarget> _aimTargetPool;

    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _dummyFilter;
    private EcsFilter             _playerFilter;
    private EcsPool<ViewRadius>   _viewRadiusPool;
    private EcsWorld              _world;


    public void Init(IEcsSystems systems) {
      _world        = systems.GetWorld();
      _dummyFilter  = _world.Filter<EcsTransform>().Inc<DummyTag>().Inc<AimTarget>().End();
      _playerFilter = _world.Filter<EcsTransform>().Inc<PlayerTag>().End();

      _displacementPool = _world.GetPool<EcsTransform>();
      _aimTargetPool    = _world.GetPool<AimTarget>();
      _viewRadiusPool   = _world.GetPool<ViewRadius>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int dummyE in _dummyFilter) {
        ref EcsTransform dummyDisp      = ref _displacementPool.Get(dummyE);
        ref AimTarget    dummyAimTarget = ref _aimTargetPool.Get(dummyE);

        int closestPlayer = -1;
        float minDistance = _viewRadiusPool.TryGet(dummyE, out ViewRadius viewRadius)
          ? viewRadius.value
          : float.MaxValue;
        Vector3 closestPosition = Vector3.zero;

        foreach (int playerE in _playerFilter) {
          ref EcsTransform playerDisp  = ref _displacementPool.Get(playerE);
          float            curDistance = Vector2.Distance(dummyDisp.position, playerDisp.position);

          if (curDistance >= minDistance)
            continue;

          closestPlayer   = playerE;
          minDistance     = curDistance;
          closestPosition = playerDisp.position;
        }

        dummyAimTarget.position = dummyDisp.position + Vector3.right;
        if (closestPlayer < 0)
          continue;

        dummyAimTarget.position = closestPosition;
      }
    }
  }
}
using Debug.Gizmos;
using Extentions;
using Leopotam.EcsLite;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Unit.Debug {
  public class DrawAimTargetSys : IEcsRunSystem, IEcsInitSystem {
    private readonly GizmosService _gizmosService;

    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _filter;
    private EcsPool<AimTarget>    _viewDirectionPool;
    private EcsPool<ViewRadius>   _viewRadiusPool;
    private EcsWorld              _world;


    public DrawAimTargetSys(GizmosService gizmosService) {
      _gizmosService = gizmosService;
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<AimTarget>().End();

      _displacementPool  = _world.GetPool<EcsTransform>();
      _viewDirectionPool = _world.GetPool<AimTarget>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        AimTarget aimTarget = _viewDirectionPool.Get(e);
        Vector3   origin    = _displacementPool.TryGet(e, out EcsTransform disp) ? disp.position : Vector3.zero;

        _gizmosService.Draw(() => aimTarget.DrawGizmos(origin));
      }
    }
  }
}
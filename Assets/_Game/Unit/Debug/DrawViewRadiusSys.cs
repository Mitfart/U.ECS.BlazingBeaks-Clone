using Debug.Gizmos;
using Extentions;
using Leopotam.EcsLite;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Unit.Debug {
  public class DrawViewRadiusSys : IEcsRunSystem, IEcsInitSystem {
    private readonly GizmosService _gizmosService;

    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _filter;
    private EcsPool<ViewRadius>   _viewRadiusPool;
    private EcsWorld              _world;


    public DrawViewRadiusSys(GizmosService gizmosService) {
      _gizmosService = gizmosService;
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<ViewRadius>().End();

      _displacementPool = _world.GetPool<EcsTransform>();
      _viewRadiusPool   = _world.GetPool<ViewRadius>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ViewRadius viewRadius = _viewRadiusPool.Get(e);
        Vector3    origin     = _displacementPool.TryGet(e, out EcsTransform disp) ? disp.position : Vector3.zero;

        _gizmosService.Draw(() => { viewRadius.DrawGizmos(origin); });
      }
    }
  }
}
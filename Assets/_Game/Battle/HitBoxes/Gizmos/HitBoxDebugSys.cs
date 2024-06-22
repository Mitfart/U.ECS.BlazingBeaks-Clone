using Debug.Gizmos;
using Extentions;
using HitBoxes.Comps;
using Leopotam.EcsLite;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace HitBoxes.Gizmos {
  public class HitBoxDebugSys : IEcsRunSystem, IEcsInitSystem {
    private readonly GizmosService _gizmosService;

    private EcsWorld  _world;
    private EcsFilter _filter;

    private EcsPool<HitBox>       _hitBoxPool;
    private EcsPool<EcsTransform> _ecsTransformPool;


    public HitBoxDebugSys(GizmosService gizmosService) {
      _gizmosService = gizmosService;
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<HitBox>().End();

      _hitBoxPool       = _world.GetPool<HitBox>();
      _ecsTransformPool = _world.GetPool<EcsTransform>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        Area      area   = _hitBoxPool.Get(e).Area;
        Matrix4x4 matrix = _ecsTransformPool.TryGet(e, out EcsTransform transform) ? transform.Matrix : default;

        _gizmosService.Draw(() => { area.DrawGizmos(matrix, Color.red); });
      }
    }
  }
}
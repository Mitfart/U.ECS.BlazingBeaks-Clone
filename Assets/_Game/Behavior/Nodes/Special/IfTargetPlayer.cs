using Behavior.Nodes.Structural;
using Extentions;
using Leopotam.EcsLite;
using Player;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Behavior.Nodes.Special.Movement {
  public class IfTargetPlayer : ConditionNode {
    private EcsPool<AimTarget> _aimTargetPool;
    private EcsFilter          _playerFilter;

    private EcsPool<EcsTransform> _transformPool;
    private EcsPool<ViewRadius>   _viewRadiusPool;



    public IfTargetPlayer(EcsBehNode @true, EcsBehNode @false) : base(@true, @false) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _playerFilter ??= world.Filter<PlayerTag>().Inc<EcsTransform>().End();

      _transformPool  ??= world.GetPool<EcsTransform>();
      _viewRadiusPool ??= world.GetPool<ViewRadius>();
      _aimTargetPool  ??= world.GetPool<AimTarget>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      ref EcsTransform transform = ref _transformPool.Get(e);
      ref AimTarget    aimTarget = ref _aimTargetPool.Get(e);

      int     closestPlayerE = -1;
      float   minDistance = _viewRadiusPool.TryGet(e, out ViewRadius viewRadius) ? viewRadius.value : float.MaxValue;
      Vector3 closestPosition = Vector3.zero;


      foreach (int playerE in _playerFilter) {
        EcsTransform playerTransform = _transformPool.Get(playerE);
        float        curDistance     = Vector2.Distance(transform.position, playerTransform.position);

        if (curDistance >= minDistance)
          continue;

        closestPlayerE  = playerE;
        minDistance     = curDistance;
        closestPosition = playerTransform.position;
      }


      if (closestPlayerE < 0)
        return False.Run(e, world);


      aimTarget.Target   = world.PackEntity(closestPlayerE);
      aimTarget.position = closestPosition;

      return True.Run(e, world);
    }
  }
}
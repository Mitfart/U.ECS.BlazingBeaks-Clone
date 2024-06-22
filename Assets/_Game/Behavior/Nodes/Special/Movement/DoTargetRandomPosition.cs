using Leopotam.EcsLite;
using Unit.Comps;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Behavior.Nodes.Special.Movement {
  public class DoTargetRandomPosition : EcsBehNode {
    private EcsPool<AimTarget> _aimTargetPool;

    private EcsPool<EcsTransform> _ecsTransformPool;
    private EcsFilter             _filter;

    private Vector3             _targetPosition;
    private EcsPool<ViewRadius> _viewRadiusPool;



    public DoTargetRandomPosition(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _filter ??= world.Filter<EcsTransform>().Inc<AimTarget>().Inc<ViewRadius>().End();

      _ecsTransformPool ??= world.GetPool<EcsTransform>();
      _aimTargetPool    ??= world.GetPool<AimTarget>();
      _viewRadiusPool   ??= world.GetPool<ViewRadius>();

      ref EcsTransform transform  = ref _ecsTransformPool.Get(e);
      ref ViewRadius   viewRadius = ref _viewRadiusPool.Get(e);

      _targetPosition = transform.position + (Vector3) Random.insideUnitCircle * viewRadius.value;
    }

    protected override State OnRun(int e, EcsWorld world) {
      ref EcsTransform transform = ref _ecsTransformPool.Get(e);
      ref AimTarget    aimTarget = ref _aimTargetPool.Get(e);

      aimTarget.position = _targetPosition;

      if (Vector2.Distance(transform.position, aimTarget.position) <= .0001f)
        return base.OnRun(e, world);

      return State.Run;
    }
  }
}
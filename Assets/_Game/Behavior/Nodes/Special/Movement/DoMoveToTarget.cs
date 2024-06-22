using _TEST;
using Leopotam.EcsLite;
using Movement.Comps;
using Unit.Comps;
using UnityRef.Transform.Comp;

namespace Behavior.Nodes.Special.Movement {
  public class DoMoveToTarget : EcsBehNode {
    private EcsPool<AimTarget>     _aimTargetPool;
    private EcsPool<EcsTransform>  _ecsTransformPool;
    private EcsFilter              _filter;
    private EcsPool<MoveDirection> _moveDirectionPool;



    public DoMoveToTarget(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _filter ??= world.Filter<DummyTag>().Inc<AimTarget>().Inc<MoveDirection>().Inc<EcsTransform>().End();

      _aimTargetPool     ??= world.GetPool<AimTarget>();
      _ecsTransformPool  ??= world.GetPool<EcsTransform>();
      _moveDirectionPool ??= world.GetPool<MoveDirection>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      ref AimTarget     aimTarget = ref _aimTargetPool.Get(e);
      ref EcsTransform  transform = ref _ecsTransformPool.Get(e);
      ref MoveDirection moveDir   = ref _moveDirectionPool.Get(e);

      if (aimTarget.Target.Unpack(world, out int _))
        moveDir.value = (aimTarget.position - transform.position).normalized;

      return State.Run;
    }
  }
}
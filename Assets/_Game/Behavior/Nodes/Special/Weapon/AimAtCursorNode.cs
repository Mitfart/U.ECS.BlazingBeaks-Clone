using Leopotam.EcsLite;
using Unit.Comps;
using Utils;

namespace Behavior.Nodes.Special.Weapon {
  public class AimAtCursorNode : EcsBehNode {
    private EcsPool<AimTarget> _aimTargetPool;



    public AimAtCursorNode(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _aimTargetPool ??= world.GetPool<AimTarget>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      ref AimTarget aimTarget = ref _aimTargetPool.Get(e);
      aimTarget.position = MouseUtils.WorldPos2D();

      return base.OnRun(e, world);
    }
  }
}
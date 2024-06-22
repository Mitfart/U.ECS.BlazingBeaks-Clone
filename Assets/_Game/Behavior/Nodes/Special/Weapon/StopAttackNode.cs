using Extentions;
using Leopotam.EcsLite;
using Weapon.Attack.Comps;

namespace Behavior.Nodes.Special.Weapon {
  public class StopAttackNode : EcsBehNode {
    private EcsPool<WantAttack> _wantShootPool;



    public StopAttackNode(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _wantShootPool ??= world.GetPool<WantAttack>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      _wantShootPool.TryDel(e);
      return base.OnRun(e, world);
    }
  }
}
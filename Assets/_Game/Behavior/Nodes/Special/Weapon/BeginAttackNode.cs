using Extentions;
using Leopotam.EcsLite;
using Weapon.Attack.Comps;

namespace Behavior.Nodes.Special.Weapon {
  public class BeginAttackNode : EcsBehNode {
    private EcsPool<WantAttack> _wantShootPool;



    public BeginAttackNode(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _wantShootPool ??= world.GetPool<WantAttack>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      _wantShootPool.TryAdd(e);
      return base.OnRun(e, world);
    }
  }
}
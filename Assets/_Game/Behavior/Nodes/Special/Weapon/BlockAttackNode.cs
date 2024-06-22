using Extentions;
using Leopotam.EcsLite;
using Weapon.Attack.Comps;

namespace Behavior.Nodes.Special.Weapon {
  public class BlockAttackNode : EcsBehNode {
    private EcsPool<BlockAttack> _blockAttackPool;



    public BlockAttackNode(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _blockAttackPool ??= world.GetPool<BlockAttack>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      _blockAttackPool.TryAdd(e);
      return base.OnRun(e, world);
    }
  }
}
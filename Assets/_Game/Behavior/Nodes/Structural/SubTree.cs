using Leopotam.EcsLite;

namespace Behavior.Nodes.Structural {
  public class SubTree : EcsBehNode {
    private readonly EcsBehTree _behTree;



    public SubTree(EcsBehTree behTree) {
      _behTree = behTree;
    }

    protected override State OnRun(int e, EcsWorld world) {
      return _behTree.Run(e, world);
    }
  }
}
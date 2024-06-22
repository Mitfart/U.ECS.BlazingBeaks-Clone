using Leopotam.EcsLite;

namespace Behavior.Nodes.Structural {
  public class DoRepeat : EcsBehNode {
    public DoRepeat(params EcsBehNode[] childNodes) : base(childNodes) { }

    protected override State OnRun(int e, EcsWorld world) {
      base.OnRun(e, world);
      return State.Run;
    }
  }
}
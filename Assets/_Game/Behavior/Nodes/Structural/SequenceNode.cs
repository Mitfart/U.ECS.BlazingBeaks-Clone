using Leopotam.EcsLite;

namespace Behavior.Nodes.Structural {
  public class SequenceNode : EcsBehNode {
    public SequenceNode(params EcsBehNode[] childNodes) : base(childNodes) { }

    protected override State OnRun(int e, EcsWorld world) {
      foreach (EcsBehNode node in ChildNodes) {
        State childState = node.Run(e, world);

        if (childState == State.Success)
          continue;

        return childState;
      }

      return State.Success;
    }
  }
}
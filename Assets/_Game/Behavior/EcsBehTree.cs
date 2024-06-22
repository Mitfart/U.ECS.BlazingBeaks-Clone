using Behavior.Nodes;
using Leopotam.EcsLite;

namespace Behavior {
  public class EcsBehTree {
    public EcsBehNode.State State { get; private set; } = EcsBehNode.State.Run;
    public EcsBehNode       Entry { get; }



    public EcsBehTree(EcsBehNode entryNode) {
      Entry = entryNode;
    }

    public EcsBehNode.State Run(int e, EcsWorld world) {
      if (State == EcsBehNode.State.Run)
        State = Entry.Run(e, world);

      return State;
    }
  }
}
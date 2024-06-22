using Leopotam.EcsLite;

namespace Behavior.Nodes {
  public abstract class EcsBehNode {
    public enum State {
      Run,
      Success,
      Fail
    }

    public    bool         IsActive   { get; private set; }
    public    State        CurState   { get; private set; } = State.Run;
    protected EcsBehNode[] ChildNodes { get; }



    protected EcsBehNode(params EcsBehNode[] childNodes) {
      ChildNodes = childNodes;
    }


    public State Run(int e, EcsWorld world) {
      Begin(e, world);
      CurState = OnRun(e, world);
      Finish(e, world);

      return CurState;
    }

    private void Begin(int e, EcsWorld world) {
      if (IsActive)
        return;

      IsActive = true;
      OnBegin(e, world);
    }

    private void Finish(int e, EcsWorld world) {
      if (CurState == State.Run)
        return;

      OnFinish(e, world);
      IsActive = false;
    }



    protected virtual State OnRun(int e, EcsWorld world) {
      State finalState = State.Success;

      foreach (EcsBehNode node in ChildNodes) {
        State childState = node.Run(e, world);

        if (childState == State.Run)
          finalState = State.Run;
      }

      return finalState;
    }

    protected virtual void OnBegin(int  e, EcsWorld world) { }
    protected virtual void OnFinish(int e, EcsWorld world) { }
  }
}
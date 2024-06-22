using Mitfart.LeoECSLite.UniLeo.Providers;

namespace Behavior.ECS {
  public abstract class BehaviorEcsAdapterProv : ScrComponent<Behavior> {
    public override Behavior Get() => new(GetBehaviorTree());

    protected abstract EcsBehTree GetBehaviorTree();
  }
}
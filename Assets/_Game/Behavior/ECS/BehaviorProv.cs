using System;
using Mitfart.LeoECSLite.UniLeo.Providers;

namespace Behavior.ECS {
  public class BehaviorProv : EcsScrProvider<Behavior, BehaviorEcsAdapterProv> { }

  [Serializable]

  public struct Behavior {
    public EcsBehTree tree;

    public Behavior(EcsBehTree behTree) {
      tree = behTree;
    }
  }
}
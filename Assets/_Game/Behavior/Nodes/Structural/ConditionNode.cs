using System;
using Leopotam.EcsLite;

namespace Behavior.Nodes.Structural {
  public class ConditionNode : EcsBehNode {
    protected EcsBehNode True      { get; }
    protected EcsBehNode False     { get; }
    protected Func<bool> Condition { get; }



    public ConditionNode(EcsBehNode @true, EcsBehNode @false, Func<bool> condition = null) : base(@true, @false) {
      True      = @true;
      False     = @false;
      Condition = condition;
    }


    protected override State OnRun(int e, EcsWorld world) {
      return Condition.Invoke()
        ? True.Run(e, world)
        : False.Run(e, world);
    }
  }
}
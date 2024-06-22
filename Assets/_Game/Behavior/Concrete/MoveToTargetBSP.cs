using Behavior.ECS;
using Behavior.Nodes.Special.Movement;
using Behavior.Nodes.Structural;
using UnityEngine;

namespace Behavior.Concrete {
  [CreateAssetMenu]
  public class MoveToTargetBSP : BehaviorEcsAdapterProv {
    [SerializeField][Min(0f)] private float waitTime = .25f;


    protected override EcsBehTree GetBehaviorTree() {
      return new EcsBehTree(
        new DoRepeat(
          new IfTargetPlayer(
            new DoMoveToTarget(),
            new DoWait(
              waitTime,
              new DoTargetRandomPosition(),
              new DoMoveToTarget()
            )
          )
        )
      );
    }
  }
}
using Extentions;
using Movement.Systems;
using UnityRef.Transform.Extras.MinRotation;
using Zenject;

namespace Movement {
  public class MovementSystems : EcsSystemsPack {
    public MovementSystems(DiContainer container) : base(container) {
      Add<DirectionInputSys>();

      Add<TransformMovementSys>();
      Add<PhysicsMovementSys>();

      Add<MinRotationSys>();
    }
  }
}
using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Movement.Comps;
using UnityEngine;

namespace Movement._CombinedProviders.CharacterMovement {
  [DisallowMultipleComponent]
  public class CharacterMovementProv : EcsScrProvider<CharacterMovement, EcsAdapterCharacterMovement> {
    public override void Convert(int e, EcsWorld world) {
      CharacterMovement v = scrComponent.Get();

      world.GetPool<Speed>().Set(e, new Speed { value               = v.MaxSpeed });
      world.GetPool<Acceleration>().Set(e, new Acceleration { value = v.Accel, dotMult = v.AccelDotFactor });
      world.GetPool<MaxAcceleration>()
           .Set(e, new MaxAcceleration { value = v.MaxAccel, dotMult = v.MaxAccelDotFactor });
      world.GetPool<MoveScale>().Set(e, new MoveScale { value = v.Scale });
      world.GetPool<MoveDirection>().Set(e);

      Destroy(this);
    }
  }
}
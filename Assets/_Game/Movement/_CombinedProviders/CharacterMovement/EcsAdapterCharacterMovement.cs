using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Movement._CombinedProviders.CharacterMovement {
  [CreateAssetMenu(fileName = "new MoveParams", menuName = "SCR/Movement/new MoveParams")]
  public class EcsAdapterCharacterMovement : ScrComponent<CharacterMovement> { }

  [Serializable]
  public struct CharacterMovement {
    [field: SerializeField] public float          MaxSpeed          { get; private set; }
    [field: SerializeField] public float          Accel             { get; private set; }
    [field: SerializeField] public AnimationCurve AccelDotFactor    { get; private set; }
    [field: SerializeField] public float          MaxAccel          { get; private set; }
    [field: SerializeField] public AnimationCurve MaxAccelDotFactor { get; private set; }
    [field: SerializeField] public float          Scale             { get; private set; }
  }
}
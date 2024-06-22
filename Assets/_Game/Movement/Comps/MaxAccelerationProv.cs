using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Movement.Comps {
  [DisallowMultipleComponent] public class MaxAccelerationProv : EcsProvider<MaxAcceleration> { }

  [Serializable]

  public struct MaxAcceleration {
    public float          value;
    public AnimationCurve dotMult;
  }
}
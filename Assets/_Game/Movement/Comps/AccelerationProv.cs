using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Movement.Comps {
  [DisallowMultipleComponent] public class AccelerationProv : EcsProvider<Acceleration> { }

  [Serializable]

  public struct Acceleration {
    public float          value;
    public AnimationCurve dotMult;
  }
}
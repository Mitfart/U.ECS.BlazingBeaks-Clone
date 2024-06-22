using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Movement.Comps {
  [DisallowMultipleComponent] public class MoveScaleProv : EcsProvider<MoveScale> { }

  [Serializable]

  public struct MoveScale {
    public float value;
  }
}
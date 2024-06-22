using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace UnityRef.Transform.Extras.MinRotation {
  [DisallowMultipleComponent] public class MinRotationProv : EcsProvider<MinRotation> { }

  [Serializable]

  public struct MinRotation {
    public int value;
  }
}
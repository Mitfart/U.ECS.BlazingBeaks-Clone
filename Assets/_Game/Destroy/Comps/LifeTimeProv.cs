using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Destroy.Comps {
  [DisallowMultipleComponent] public class LifeTimeProv : EcsProvider<LifeTime> { }

  [Serializable]

  public struct LifeTime {
    public float value;
  }
}
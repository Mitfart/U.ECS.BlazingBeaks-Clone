using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace UI {
  [DisallowMultipleComponent] public class HealthBarProv : EcsProvider<HealthBar> { }

  [Serializable]

  public struct HealthBar {
    public ProgressBar.ProgressBar bar;
  }
}
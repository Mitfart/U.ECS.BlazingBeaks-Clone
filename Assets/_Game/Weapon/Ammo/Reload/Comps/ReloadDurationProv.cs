using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Weapon.Ammo.Reload.Comps {
  [DisallowMultipleComponent] public class ReloadDurationProv : EcsProvider<ReloadDuration> { }

  [Serializable]

  public struct ReloadDuration {
    public                   float duration;
    [HideInInspector] public float startTime;
  }
}
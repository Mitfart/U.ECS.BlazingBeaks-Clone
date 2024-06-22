using System;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unit.Debug;
using UnityEngine;

namespace Unit.Comps {
  [DisallowMultipleComponent]
  public class AimTargetProv : EcsProvider<AimTarget> {
    private void OnDrawGizmosSelected() {
      component.DrawGizmos(transform.position);
    }
  }

  [Serializable]

  public struct AimTarget {
    public Vector3         position;
    public EcsPackedEntity Target;
  }
}
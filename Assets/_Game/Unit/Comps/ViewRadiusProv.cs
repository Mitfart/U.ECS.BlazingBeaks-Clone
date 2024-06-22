using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unit.Debug;
using UnityEngine;

namespace Unit.Comps {
  [DisallowMultipleComponent]
  public class ViewRadiusProv : EcsProvider<ViewRadius> {
    private void OnDrawGizmosSelected() {
      component.DrawGizmos(transform.position);
    }
  }

  [Serializable]

  public struct ViewRadius {
    public float value;
  }
}
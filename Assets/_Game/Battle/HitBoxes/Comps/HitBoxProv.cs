using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace HitBoxes.Comps {
  [DisallowMultipleComponent]
  public class HitBoxProv : EcsProvider<HitBox> {
    private void OnDrawGizmosSelected() {
      Area area = component.Area;
      area.origin += transform.position;

      UnityEngine.Gizmos.color = Color.red;
      UnityEngine.Gizmos.DrawWireCube(area.origin, area.size);
    }
  }

  [Serializable]

  public struct HitBox {
    [field: SerializeField] public Area      Area      { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
  }
}
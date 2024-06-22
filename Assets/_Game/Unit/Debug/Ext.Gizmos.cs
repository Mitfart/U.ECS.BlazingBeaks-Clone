using Unit.Comps;
using UnityEngine;

namespace Unit.Debug {
  public static class Ext {
    public static void DrawGizmos(this ViewRadius component, Vector3 origin) {
      Gizmos.color = Color.cyan;
      Gizmos.DrawWireSphere(origin, component.value);
    }

    public static void DrawGizmos(this AimTarget component, Vector3 origin) {
      Gizmos.color = Color.cyan;
      Gizmos.DrawLine(origin, component.position);
    }
  }
}
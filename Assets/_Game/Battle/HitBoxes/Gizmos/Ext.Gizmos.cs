using Extentions;
using HitBoxes.Comps;
using Leopotam.EcsLite;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace HitBoxes.Gizmos {
  public static class ExtGizmos {
    public static void DrawGizmos(this Area area, Matrix4x4 matrix, Color? color = null) {
      const float colorFillOpacity   = .25f;
      const float colorBorderOpacity = 1f;

      Color col = color ?? Color.white;

      col.a                    = colorFillOpacity;
      UnityEngine.Gizmos.color = col;

      UnityEngine.Gizmos.matrix = matrix;
      UnityEngine.Gizmos.DrawCube(area.origin, area.size);

      col.a                    = colorBorderOpacity;
      UnityEngine.Gizmos.color = col;
      UnityEngine.Gizmos.DrawWireCube(area.origin, area.size);
    }

    public static void DrawGizmos(this HitEvent component) {
      if (!component.dealer.Unpack(out EcsWorld world, out int de))
        return;
      if (!component.taker.Unpack(out EcsWorld _, out int te))
        return;

      EcsPool<EcsTransform> displacementPool = world.GetPool<EcsTransform>();
      if (!displacementPool.TryGet(de, out EcsTransform ddisp))
        return;
      if (!displacementPool.TryGet(te, out EcsTransform tdisp))
        return;

      UnityEngine.Gizmos.color = Color.yellow;
      UnityEngine.Gizmos.DrawLine(ddisp.position, tdisp.position);
    }
  }
}
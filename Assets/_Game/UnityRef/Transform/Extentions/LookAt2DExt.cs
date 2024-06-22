using UnityEngine;
using UnityRef.Transform.Comp;

namespace UnityRef.Transform.Extentions {
  public static class LookAt2DExt {
    public static ref EcsTransform LookAt2D(ref this EcsTransform cur, Vector3 target, Vector3? eye = null) {
      float signedAngle = Vector2.SignedAngle(eye ?? cur.Up, target - cur.position);

      if (Mathf.Abs(signedAngle) < 1e-3f)
        return ref cur;

      Vector3 angles = cur.EulerAngles;
      angles.z        += signedAngle;
      cur.EulerAngles =  angles;
      return ref cur;
    }

    public static ref EcsTransform LookAt2D(ref this EcsTransform cur, EcsTransform target, Vector3? eye = null) {
      return ref cur.LookAt2D(target.position, eye);
    }
  }
}
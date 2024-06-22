using UnityRef.Transform.Comp;

namespace UnityRef.Transform.Extentions {
  public static class SetExt {
    public static UnityEngine.Transform Set(this UnityEngine.Transform cur, EcsTransform ecsTransform) {
      cur.position   = ecsTransform.position;
      cur.rotation   = ecsTransform.rotation;
      cur.localScale = ecsTransform.scale;
      return cur;
    }

    public static ref EcsTransform Set(ref this EcsTransform cur, UnityEngine.Transform transform) {
      cur.position = transform.position;
      cur.rotation = transform.rotation;
      cur.scale    = transform.localScale;
      return ref cur;
    }
  }
}
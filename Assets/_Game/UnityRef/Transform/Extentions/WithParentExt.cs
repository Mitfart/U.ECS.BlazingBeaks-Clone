using UnityEngine;
using UnityRef.Transform.Comp;

namespace UnityRef.Transform.Extentions {
  public static class WithParentExt {
    public static EcsTransform WithParent(ref this EcsTransform cur, EcsTransform ecsTransform) {
      var rot = Quaternion.Euler(ecsTransform.rotation.eulerAngles + cur.EulerAngles);
      return new EcsTransform(ecsTransform.position + ecsTransform.rotation * cur.position, rot, cur.scale);
    }
  }
}
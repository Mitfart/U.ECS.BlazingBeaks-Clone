using System;
using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unity.VisualScripting;
using UnityRef.Transform.Extentions;

namespace UnityRef.Transform.Comp {
  public class EcsTransformProv : BaseEcsProvider {
    public override void Convert(int e, EcsWorld world) {
      if (transform.IsUnityNull())
        throw new NullReferenceException("Value cant be Null!");

      world.GetPool<EcsTransform>().Set(e).Set(transform);
      world.GetPool<URefTransform>().Set(e).Value = transform;

      Destroy(this);
    }
  }
}
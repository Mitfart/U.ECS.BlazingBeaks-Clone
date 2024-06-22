using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unity.VisualScripting;
using UnityEngine;

namespace Extentions.ConvertExtensions.Providers {
  public abstract class EcsUnityURefProv<TMono, TComp> : BaseEcsProvider, ISerializationCallbackReceiver
    where TMono : Component
    where TComp : struct, IEcsURef<TMono> {
    public TMono component;

    public virtual void OnBeforeSerialize() {
      if (component.IsUnityNull()
       && TryGetComponent(out TMono comp))
        component = comp;
    }

    public virtual void OnAfterDeserialize() { }

    public override void Convert(int e, EcsWorld world) {
      if (component.IsUnityNull()
       && TryGetComponent(out TMono comp))
        component = comp;
      world.GetPool<TComp>().Set(e).Value = component;

      Destroy(this);
    }
  }

  public interface IEcsURef<T> where T : Component {
    public T Value { get; set; }
  }
}
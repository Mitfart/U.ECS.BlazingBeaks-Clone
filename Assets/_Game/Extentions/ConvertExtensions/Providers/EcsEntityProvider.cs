using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unity.VisualScripting;

namespace Extentions.ConvertExtensions.Providers {
  public abstract class EcsEntityProvider<T, TProvider> : BaseEcsProvider
    where T : struct
    where TProvider : BaseEcsProvider {
    public TProvider provider;

    public override void Convert(int e, EcsWorld world) {
      var component = new T();
      Init(ref component);

      if (!provider.IsUnityNull()
       || provider.isActiveAndEnabled)
        Process(ref component, provider.GetComponent<ConvertToEntity>());

      world.GetPool<T>().Set(e) = component;
      Destroy(this);
    }


    public abstract void Init(ref    T component);
    public abstract void Process(ref T component, ConvertToEntity provider);
  }
}
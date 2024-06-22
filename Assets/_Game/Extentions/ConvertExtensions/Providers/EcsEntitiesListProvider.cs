using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unity.VisualScripting;
using UnityEngine;

namespace Extentions.ConvertExtensions.Providers {
  public abstract class EcsEntitiesListProvider<T, TProvider> : BaseEcsProvider
    where T : struct
    where TProvider : BaseEcsProvider {
    [field: SerializeField] public List<TProvider> Providers { get; private set; }

    protected virtual void OnValidate() {
      if (Providers is { Count: > 0 })
        Providers = LinqUtility.ToHashSet(Providers).ToList();
    }

    public override void Convert(int e, EcsWorld world) {
      var component = new T();
      Init(ref component);

      foreach (TProvider provider in Providers)
        Process(ref component, provider);

      world.GetPool<T>().Set(e, component);
      Destroy(this);
    }

    protected abstract void Init(ref    T component);
    protected abstract void Process(ref T component, TProvider provider);
  }
}
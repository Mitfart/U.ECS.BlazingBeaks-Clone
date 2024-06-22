using Extentions.ConvertExtensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Destroy.Systems {
  public class TestDestroySys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<Converted> _convertedPool;
    private EcsFilter          _filter;
    private EcsWorld           _world;

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<Comps.Destroy>().End();

      _convertedPool = _world.GetPool<Converted>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        if (_convertedPool.Has(e))
          Object.Destroy(_convertedPool.Get(e).gameObject);

        _world.DelEntity(e);
      }
    }
  }
}
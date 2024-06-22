using Extentions;
using Leopotam.EcsLite;

namespace Destroy.Systems {
  public class DestroyIfHealthBelowZero : IEcsFixedRunSystem, IEcsInitSystem {
    private EcsPool<Comps.Destroy> _destroyPool;
    private EcsFilter              _filter;

    private EcsPool<Health> _healthPool;
    private EcsWorld        _world;

    public void FixedRun(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref Health health = ref _healthPool.Get(e);
        if (health.value <= 0)
          _destroyPool.Add(e);
      }
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<Health>().Exc<Comps.Destroy>().End();

      _healthPool  = _world.GetPool<Health>();
      _destroyPool = _world.GetPool<Comps.Destroy>();
    }
  }
}
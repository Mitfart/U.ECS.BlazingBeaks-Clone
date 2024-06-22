using Destroy.Comps;
using Extentions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Destroy.Systems {
  public class LifeTimeDestroySys : IEcsFixedRunSystem, IEcsInitSystem {
    private EcsPool<Comps.Destroy> _destroyPool;
    private EcsFilter              _filter;

    private EcsPool<LifeTime> _lifeTimePool;
    private EcsWorld          _world;


    public void FixedRun(IEcsSystems systems) {
      //var time = Time.time;
      float delta = Time.deltaTime;

      foreach (int e in _filter) {
        ref LifeTime lifeTime = ref _lifeTimePool.Get(e);

        //if (time >= lifeTime.spawnTime + lifeTime.value) 
        if (lifeTime.value <= 0)
          _destroyPool.Add(e);
        else
          lifeTime.value -= delta;
      }
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<LifeTime>().Exc<Comps.Destroy>().End();

      _lifeTimePool = _world.GetPool<LifeTime>();
      _destroyPool  = _world.GetPool<Comps.Destroy>();
    }
  }
}
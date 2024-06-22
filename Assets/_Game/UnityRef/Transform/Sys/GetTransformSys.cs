using Leopotam.EcsLite;
using UnityRef.Transform.Comp;
using UnityRef.Transform.Extentions;

namespace UnityRef.Transform.Sys {
  public class GetTransformSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<EcsTransform>  _ecsTransformsPool;
    private EcsFilter              _filter;
    private EcsPool<URefTransform> _uRefTransformsPool;
    private EcsWorld               _world;



    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<EcsTransform>().Inc<URefTransform>().End();

      _ecsTransformsPool  = _world.GetPool<EcsTransform>();
      _uRefTransformsPool = _world.GetPool<URefTransform>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref EcsTransform  ecsTransform  = ref _ecsTransformsPool.Get(e);
        ref URefTransform uRefTransform = ref _uRefTransformsPool.Get(e);

        ecsTransform.Set(uRefTransform.Value);
      }
    }
  }
}
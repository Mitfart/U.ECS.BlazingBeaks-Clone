using Leopotam.EcsLite;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace UnityRef.Transform.Extras.MinRotation {
  public class MinRotationSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _filter;
    private EcsPool<MinRotation>  _minRotationPool;
    private EcsWorld              _world;

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<EcsTransform>().Inc<MinRotation>().End();

      _displacementPool = _world.GetPool<EcsTransform>();
      _minRotationPool  = _world.GetPool<MinRotation>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref EcsTransform displacement = ref _displacementPool.Get(e);
        ref MinRotation  minRotation  = ref _minRotationPool.Get(e);

        if (minRotation.value == 0)
          continue;

        Vector3 angles = displacement.EulerAngles;

        angles.z                 -= angles.z % minRotation.value;
        displacement.EulerAngles =  angles;
      }
    }
  }
}
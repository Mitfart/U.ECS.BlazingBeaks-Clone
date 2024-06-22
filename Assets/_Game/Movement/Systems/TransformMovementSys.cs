using Extentions;
using Leopotam.EcsLite;
using Movement.Comps;
using UnityEngine;
using UnityRef.Physics2D.Rigidbody2D;
using UnityRef.Transform.Comp;

namespace Movement.Systems {
  public class TransformMovementSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<EcsTransform> _displacementPool;
    private EcsFilter             _filter;

    private EcsPool<Speed>         _maxSpeedPool;
    private EcsPool<MoveDirection> _moveDirectionPool;
    private EcsPool<MoveScale>     _scalePool;
    private EcsWorld               _world;

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<MoveDirection>().Inc<EcsTransform>().Exc<Rigidbody2DRef>().End();

      _displacementPool  = _world.GetPool<EcsTransform>();
      _moveDirectionPool = _world.GetPool<MoveDirection>();

      _maxSpeedPool = _world.GetPool<Speed>();
      _scalePool    = _world.GetPool<MoveScale>();
    }

    public void Run(IEcsSystems systems) {
      float delta = Time.deltaTime;

      foreach (int e in _filter) {
        ref EcsTransform  displacement  = ref _displacementPool.Get(e);
        ref MoveDirection moveDirection = ref _moveDirectionPool.Get(e);

        float speed = _maxSpeedPool.TryGet(e, out Speed ms) ? ms.value : 1f;
        float scale = _scalePool.TryGet(e, out MoveScale s) ? s.value : 1f;

        displacement.position += moveDirection.value * (speed * scale * delta);
      }
    }
  }
}
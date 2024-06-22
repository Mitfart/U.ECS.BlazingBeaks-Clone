using Extentions;
using Leopotam.EcsLite;
using Movement.Comps;
using UnityEngine;
using UnityRef.Physics2D.Rigidbody2D;

namespace Movement.Systems {
  public class PhysicsMovementSys : IEcsFixedRunSystem, IEcsInitSystem {
    private EcsPool<Acceleration>    _accelerationPool;
    private EcsFilter                _filter;
    private EcsPool<MaxAcceleration> _maxAccelerationPool;

    private EcsPool<Speed>         _maxSpeedPool;
    private EcsPool<MoveDirection> _moveDirectionPool;

    private EcsPool<Rigidbody2DRef> _rigidbodyLinkPool;
    private EcsPool<MoveScale>      _scalePool;
    private EcsWorld                _world;


    public void FixedRun(IEcsSystems systems) {
      float delta = Time.deltaTime;

      foreach (int e in _filter) {
        ref MoveDirection input = ref _moveDirectionPool.Get(e);
        Rigidbody2D       rb    = _rigidbodyLinkPool.Get(e).Value;


        Vector3 goalDir = input.value.normalized;

        float scale    = _scalePool.TryGet(e, out MoveScale s) ? s.value : 1f;
        float maxSpeed = _maxSpeedPool.TryGet(e, out Speed ms) ? ms.value : float.MaxValue;

        Vector2 curVel  = rb.velocity / scale;
        Vector3 goalVel = goalDir     * maxSpeed;

        Vector2 curDir = curVel.normalized;
        float   velDot = Vector2.Dot(goalDir, curDir);


        float accel = _accelerationPool.TryGet(e, out Acceleration acc)
          ? acc.value * acc.dotMult.Evaluate(velDot)
          : float.MaxValue;
        float maxAccel = _maxAccelerationPool.TryGet(e, out MaxAcceleration mAcc)
          ? mAcc.value * mAcc.dotMult.Evaluate(velDot)
          : float.MaxValue;


        var     nextVel     = Vector2.MoveTowards(curVel, goalVel, accel * delta);
        Vector2 neededAccel = (nextVel - curVel) / delta;


        neededAccel = Vector2.ClampMagnitude(neededAccel, maxAccel);

        rb.AddForce(neededAccel * scale);
      }
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<MoveDirection>().Inc<Rigidbody2DRef>().End();

      _rigidbodyLinkPool = _world.GetPool<Rigidbody2DRef>();
      _moveDirectionPool = _world.GetPool<MoveDirection>();

      _maxSpeedPool        = _world.GetPool<Speed>();
      _accelerationPool    = _world.GetPool<Acceleration>();
      _maxAccelerationPool = _world.GetPool<MaxAcceleration>();
      _scalePool           = _world.GetPool<MoveScale>();
    }
  }
}
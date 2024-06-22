using Events;
using HitBoxes.Comps;
using Leopotam.EcsLite;

public class TakeDamageByHitEventSys : IEcsRunSystem {
  private readonly EventsBus _eventsBus;

  private EcsPool<Damage> _damagePool;
  private EcsPool<Health> _healthPool;

  public TakeDamageByHitEventSys(EventsBus eventsBus) {
    _eventsBus = eventsBus;
  }

  public void Run(IEcsSystems systems) {
    foreach (int ev in _eventsBus.GetEventBodies(out EcsPool<HitEvent> hitEventPoll)) {
      ref HitEvent hitEvent = ref hitEventPoll.Get(ev);

      if (!hitEvent.dealer.Unpack(out EcsWorld world, out int de))
        return;
      if (!hitEvent.taker.Unpack(out EcsWorld _, out int te))
        return;

      _damagePool ??= world.GetPool<Damage>();
      _healthPool ??= world.GetPool<Health>();

      if (!_healthPool.Has(te)
       || !_damagePool.Has(de))
        return;

      _healthPool.Get(te).value -= _damagePool.Get(de).value;
    }
  }
}
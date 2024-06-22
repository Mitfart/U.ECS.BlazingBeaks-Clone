using Events;
using Extentions;
using HitBoxes.Comps;
using Leopotam.EcsLite;

namespace HitBoxes.Sys {
  public class DestroyDealerByHitEventSys : IEcsRunSystem {
    private readonly EventsBus _eventsBus;

    public DestroyDealerByHitEventSys(EventsBus eventsBus) {
      _eventsBus = eventsBus;
    }

    public void Run(IEcsSystems systems) {
      foreach (int ev in _eventsBus.GetEventBodies(out EcsPool<HitEvent> hitEventPoll)) {
        ref HitEvent hitEvent = ref hitEventPoll.Get(ev);

        if (!hitEvent.dealer.Unpack(out EcsWorld world, out int de))
          return;

        world.GetPool<Destroy.Comps.Destroy>().TryAdd(de);
      }
    }
  }
}
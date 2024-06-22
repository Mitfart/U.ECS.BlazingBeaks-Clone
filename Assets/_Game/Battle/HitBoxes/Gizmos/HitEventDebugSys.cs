using Debug.Gizmos;
using Events;
using HitBoxes.Comps;
using Leopotam.EcsLite;

namespace HitBoxes.Gizmos {
  public class HitEventDebugSys : IEcsRunSystem {
    private readonly EventsBus     _eventsBus;
    private readonly GizmosService _gizmosService;


    public HitEventDebugSys(GizmosService gizmosService, EventsBus eventsBus) {
      _gizmosService = gizmosService;
      _eventsBus     = eventsBus;
    }

    public void Run(IEcsSystems systems) {
      foreach (int ev in _eventsBus.GetEventBodies(out EcsPool<HitEvent> hitEventPoll)) {
        HitEvent hitEvent = hitEventPoll.Get(ev);

        _gizmosService.Draw(() => { hitEvent.DrawGizmos(); });
      }
    }
  }
}
using Extentions;
using HitBoxes.Comps;
using Zenject;

namespace Events {
  public class DestroyEvents : EcsSystemsPack {
    private const int EVENTS_CAPACITY = 128;

    public DestroyEvents(EventsBus eventsBus, DiContainer container) : base(container) {
      AddByInstance(
        eventsBus
         .GetDestroyEventsSystem(EVENTS_CAPACITY)
         .IncReplicant<HitEvent>()
      );
    }
  }
}
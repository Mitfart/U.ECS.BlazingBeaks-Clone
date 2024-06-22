using Events;
using Extentions;
using HitBoxes.Comps;
using Leopotam.EcsLite;
using UnityEngine;
using UnityRef.Animator;

namespace HitBoxes.Sys {
  public class PlayHitAnimation : IEcsRunSystem {
    private static readonly int Hit = Animator.StringToHash("Hit");

    private readonly EventsBus _eventsBus;

    public PlayHitAnimation(EventsBus eventsBus) {
      _eventsBus = eventsBus;
    }

    public void Run(IEcsSystems systems) {
      EcsFilter filter = _eventsBus.GetEventBodies(out EcsPool<HitEvent> hitEventPoll);

      foreach (int ev in filter) {
        ref HitEvent hitEvent = ref hitEventPoll.Get(ev);

        if (!hitEvent.taker.Unpack(out EcsWorld world, out int te))
          return;

        if (!world.GetPool<AnimatorURef>().TryGet(te, out AnimatorURef animatorLink))
          return;

        animatorLink.Value.SetTrigger(Hit);
      }
    }
  }
}
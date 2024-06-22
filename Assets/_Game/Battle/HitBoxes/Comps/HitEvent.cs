using System;
using Events;
using Leopotam.EcsLite;

namespace HitBoxes.Comps {
  [Serializable]
  public struct HitEvent : IEvent {
    public EcsPackedEntityWithWorld dealer;
    public EcsPackedEntityWithWorld taker;
  }
}
using Extentions;
using HitBoxes.Gizmos;
using HitBoxes.Sys;
using UI;
using Zenject;

public class BattleSystems : EcsSystemsPack {
  public BattleSystems(DiContainer container) : base(container) {
    Add<HitReceiveSys>();
#if UNITY_EDITOR
    Add<HitBoxDebugSys>();
    Add<HitEventDebugSys>();
#endif
    Add<TakeDamageByHitEventSys>();
    Add<DestroyDealerByHitEventSys>();

    Add<PlayHitAnimation>();
    Add<SetHealthUISys>();
  }
}
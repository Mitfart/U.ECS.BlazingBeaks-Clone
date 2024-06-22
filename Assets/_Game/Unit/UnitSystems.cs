using Extentions;
using Unit.Debug;
using Unit.Sys;
using Zenject;

namespace Unit {
  public class UnitSystems : EcsSystemsPack {
    public UnitSystems(DiContainer container) : base(container) {
      Add<TurnToAimTargetSys>();

#if UNITY_EDITOR
      Add<DrawAimTargetSys>();
      Add<DrawViewRadiusSys>();
#endif
    }
  }
}
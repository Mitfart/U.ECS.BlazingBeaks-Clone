using _TEST;
using Behavior.ECS;
using Camera.Shake;
using Destroy;
using Events;
using Extentions;
using LeoECSLite.UtilityAI.Test;
using Mitfart.LeoECSLite.UnityIntegration;
using Movement;
using Unit;
using UnityRef;
using Weapon;
using Zenject;

public class MainSystemsPack : EcsSystemsPack {
  public MainSystemsPack(DiContainer container) : base(container) {
    AddPack<DestroySystems>();

    AddPack<GetMonoDataSystems>();

    Add<TestPlayerInputSys>(); // *************************
    Add<ProcessBehaviorSys>(); // *************************
    Add<TestAISys>(); // *************************

    AddPack<MovementSystems>();
    AddPack<UnitSystems>();
    AddPack<WeaponSystems>();
    AddPack<BattleSystems>();

    AddPack<CameraShakeSystems>();

    AddPack<SetMonoDataSystems>();
    AddPack<DestroyEvents>();

    AddWorldsDebug();
  }


  private void AddWorldsDebug() {
#if UNITY_EDITOR
#if true
    AddByInstance(new EcsWorldDebugSystem(nameSettings: new NameSettings(bakeComponents: false)));
#endif
#if true
    AddByInstance(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
#if false
    AddByInstance(
      new Mitfart.LeoECSLite.UnityIntegration.EcsWorldDebugSystem(
        nameSettings: new EntityNameSettings(bakeComponents: true)
      )
    );
#endif
#endif
  }
}
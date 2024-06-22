using Events;
using Extentions;
using Weapon.Aim;
using Weapon.Ammo._base.Systems;
using Weapon.Ammo.Reload.Comps;
using Weapon.Ammo.Reload.Systems;
using Weapon.Attack.Comps;
using Weapon.Attack.Systems;
using Weapon.Projectiles.Sys;
using Zenject;

namespace Weapon {
  public class WeaponSystems : EcsSystemsPack {
    public WeaponSystems(DiContainer container) : base(container) {
      Add<AimAtTargetSys>();
      Add<ShootSys>();
      Add<DelHereSys<BlockAttack>>();

      Add<ReduceAmmoSys>();

      Add<StartReloadingSys>();
      Add<ReloadingSys>();
      Add<DelHereSys<WantReload>>();
      Add<DelHereSys<BlockReload>>();

      Add<RestoreAttackSys>();
      Add<DelHereSys<IsAttacking>>();
    }
  }
}
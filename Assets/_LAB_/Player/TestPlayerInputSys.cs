using Extentions;
using Leopotam.EcsLite;
using Player;
using Unit.Comps;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Weapon._base;
using Weapon.Ammo;
using Weapon.Ammo._base.Comps;
using Weapon.Ammo.Reload.Comps;
using Weapon.Attack.Comps;

namespace _TEST {
  public class TestPlayerInputSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<ActiveWeapons> _activeWeaponPool;
    private EcsPool<AimTarget>     _aimTargetPool;
    private EcsPool<AutoReload>    _autoReloadPool;
    private EcsFilter              _filter;
    private EcsPool<Magazine>      _magazinePool;
    private EcsPool<WantReload>    _wantReloadPool;
    private EcsPool<WantAttack>    _wantShootPool;
    private EcsWorld               _world;


    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<PlayerTag>().End();

      _activeWeaponPool = _world.GetPool<ActiveWeapons>();
      _wantShootPool    = _world.GetPool<WantAttack>();
      _wantReloadPool   = _world.GetPool<WantReload>();
      _magazinePool     = _world.GetPool<Magazine>();
      _autoReloadPool   = _world.GetPool<AutoReload>();
      _aimTargetPool    = _world.GetPool<AimTarget>();
    }

    public void Run(IEcsSystems systems) {
      Vector2 mousePos     = MouseUtils.WorldPos2D();
      bool    shootBtnDown = MouseUtils.RightBtnDown();
      bool    shootBtnUp   = MouseUtils.RightBtnUp();

      foreach (int e in _filter) {
        if (!_activeWeaponPool.Has(e))
          continue;

        foreach (EcsPackedEntity activeWeapon in _activeWeaponPool.Get(e).weapons) {
          if (!activeWeapon.Unpack(_world, out int e2))
            continue;

          ShootOrStop(e2, shootBtnDown, shootBtnUp);
          ReloadIfEmpty(e2, shootBtnDown);
          AimAtMouse(e, mousePos);
        }
      }
    }


    private void ShootOrStop(int e2, bool shootBtnDown, bool shootBtnUp) {
      if (shootBtnDown)
        _wantShootPool.TryAdd(e2);
      else if (shootBtnUp)
        _wantShootPool.TryDel(e2);
    }

    private void ReloadIfEmpty(int e2, bool shootBtnDown) {
      bool reloadKey = Keyboard.current.rKey.wasPressedThisFrame;

      bool magazineIsEmpty = _magazinePool.TryGet(e2, out Magazine magazine) && magazine.IsEmpty();
      if (reloadKey || (magazineIsEmpty && (shootBtnDown || _autoReloadPool.Has(e2))))
        _wantReloadPool.TryAdd(e2);
    }

    private void AimAtMouse(int e, Vector2 mousePos) {
      if (!_aimTargetPool.Has(e))
        return;

      ref AimTarget aimTarget = ref _aimTargetPool.Get(e);
      aimTarget.position = mousePos;
    }
  }
}
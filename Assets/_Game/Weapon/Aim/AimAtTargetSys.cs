using Leopotam.EcsLite;
using Unit.Comps;
using UnityRef.Transform.Comp;
using UnityRef.Transform.Extentions;
using Weapon._base;

namespace Weapon.Aim {
  public class AimAtTargetSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<ActiveWeapons> _activeWeaponsPool;
    private EcsPool<AimTarget>     _aimTargetPool;
    private EcsPool<EcsTransform>  _displacementPool;
    private EcsFilter              _filter;
    private EcsWorld               _world;


    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<ActiveWeapons>().Inc<EcsTransform>().Inc<AimTarget>().End();

      _activeWeaponsPool = _world.GetPool<ActiveWeapons>();
      _displacementPool  = _world.GetPool<EcsTransform>();
      _aimTargetPool     = _world.GetPool<AimTarget>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int ownerE in _filter) {
        ref ActiveWeapons activeWeapons = ref _activeWeaponsPool.Get(ownerE);
        ref AimTarget     aimTarget     = ref _aimTargetPool.Get(ownerE);

        foreach (EcsPackedEntity activeWeapon in activeWeapons.weapons) {
          if (!activeWeapon.Unpack(_world, out int weaponE))
            continue;

          if (_displacementPool.Has(weaponE))
            _displacementPool.Get(weaponE).LookAt2D(aimTarget.position);
        }
      }
    }
  }
}
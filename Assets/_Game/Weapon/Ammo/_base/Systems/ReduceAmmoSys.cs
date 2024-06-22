using Extentions;
using Leopotam.EcsLite;
using Weapon.Ammo._base.Comps;
using Weapon.Attack.Comps;

namespace Weapon.Ammo._base.Systems {
  public class ReduceAmmoSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<Comps.Ammo>  _ammoPool;
    private EcsPool<BlockAttack> _cantShootPool;
    private EcsFilter            _filter;
    private EcsPool<IsAttacking> _isShootingPool;

    private EcsPool<Magazine> _magazinePool;
    private EcsWorld          _world;

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<Magazine>().Inc<Weapon._base.Weapon>().Exc<BlockAttack>().End();

      _magazinePool   = _world.GetPool<Magazine>();
      _cantShootPool  = _world.GetPool<BlockAttack>();
      _isShootingPool = _world.GetPool<IsAttacking>();

      _ammoPool = _world.GetPool<Comps.Ammo>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter) {
        ref Magazine magazine = ref _magazinePool.Get(e);

        if (magazine.IsEmpty()) {
          _cantShootPool.TryAdd(e);
        }
        else if (_isShootingPool.Has(e)) {
          if (_ammoPool.Has(e))
            _ammoPool.Get(e).amount--;
          magazine.amount--;
        }
        else {
          _cantShootPool.TryDel(e);
        }
      }
    }
  }
}
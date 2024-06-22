using System;
using System.Collections.Generic;
using Extentions.ConvertExtensions.Providers;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;
using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;

namespace Weapon._base {
  [DisallowMultipleComponent]
  public class ActiveWeaponsProv : EcsEntitiesListProvider<ActiveWeapons, WeaponProv> {
    protected override void Init(ref ActiveWeapons component) {
      component.weapons = new List<EcsPackedEntity>(Providers.Count);
    }

    protected override void Process(ref ActiveWeapons component, WeaponProv provider) {
      component.weapons.Add(provider.GetComponent<ConvertToEntity>().Convert().Packed);
    }
  }

  [Serializable]

  public struct ActiveWeapons {
    [PackedEntity] public EcsPackedEntity          weapon;
    [PackedEntity] public EcsPackedEntityWithWorld weapon_ww;
    [PackedEntity] public List<EcsPackedEntity>    weapons;
    [PackedEntity] public EcsPackedEntity[]        weapons_array;
  }
}
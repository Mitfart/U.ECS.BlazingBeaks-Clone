using System;
using System.Collections.Generic;
using Extentions.ConvertExtensions.Providers;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;
using UnityEngine;

namespace Weapon._base {
  [DisallowMultipleComponent]
  public class WeaponsOwnerProv : EcsEntitiesListProvider<WeaponsOwner, WeaponProv> {
    protected override void Init(ref WeaponsOwner component) {
      component.weapons = new List<EcsPackedEntity>(Providers.Count);
    }

    protected override void Process(ref WeaponsOwner component, WeaponProv provider) {
      component.weapons.Add(provider.GetComponent<ConvertToEntity>().Convert().Packed);
    }
  }

  [Serializable]

  public struct WeaponsOwner {
    public List<EcsPackedEntity> weapons;
  }
}
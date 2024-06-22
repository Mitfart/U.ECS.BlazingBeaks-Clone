using System;
using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Weapon.Ammo._base.Comps {
  [DisallowMultipleComponent]
  public class MagazineProv : BaseEcsProvider {
    public int value;

    public override void Convert(int e, EcsWorld world) {
      world.GetPool<Magazine>().Set(e) = new Magazine { amount = value, size = value };

      Destroy(this);
    }
  }

  [Serializable]

  public struct Magazine {
    public int amount;
    public int size;
  }
}
using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Weapon.Ammo._base.Comps {
  [DisallowMultipleComponent] public class AmmoProv : EcsProvider<Ammo> { }

  [Serializable]

  public struct Ammo {
    public int amount;
    public int size;
  }
}
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Weapon.Ammo.Reload.Comps {
  [DisallowMultipleComponent] public class AutoReloadProv : EcsProvider<AutoReload> { }

  

  public struct AutoReload { }
}
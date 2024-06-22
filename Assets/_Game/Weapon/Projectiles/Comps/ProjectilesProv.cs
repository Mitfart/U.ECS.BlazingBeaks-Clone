using System;
using System.Collections.Generic;
using Mitfart.LeoECSLite.UniLeo;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Weapon.Projectiles.Comps {
   [DisallowMultipleComponent] 
   public class ProjectilesProv : EcsProvider<Projectiles> { }

   [Serializable]
   public struct Projectiles {
      public List<ConvertToEntity> value;
   }
}
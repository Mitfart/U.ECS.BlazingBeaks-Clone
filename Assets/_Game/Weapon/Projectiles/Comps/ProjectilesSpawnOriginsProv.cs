using System;
using System.Collections.Generic;
using System.Linq;
using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Weapon.Projectiles.Comps {
   [DisallowMultipleComponent]
   public class ProjectilesSpawnOriginsProv : BaseEcsProvider {
      public List<Transform> origins;

      public override void Convert(int e, EcsWorld world) {
         var displacements = new List<EcsTransform>(origins.Count);
         displacements.AddRange(origins.Select(origin => new EcsTransform(origin)));

         world.GetPool<ProjectilesSpawnOrigins>().Set(e).value = displacements;
      }
   }

   [Serializable]
   public struct ProjectilesSpawnOrigins {
      public List<EcsTransform> value;
   }
}
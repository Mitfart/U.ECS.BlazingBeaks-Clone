using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;
using UnityEngine;
using UnityRef.Transform.Comp;
using UnityRef.Transform.Extentions;
using Weapon.Attack.Comps;
using Weapon.Projectiles.Comps;
using Zenject;

namespace Weapon.Projectiles.Sys {
   public class ShootSys : IEcsRunSystem, IEcsInitSystem {
      private readonly DiContainer _diContainer; // *****************

      private EcsPool<EcsTransform>      _displacementPool;
      private EcsFilter                  _filter;
      private Transform                  _insContainer; // *****************
      private EcsPool<IsAttacking>       _isShootingPool;
      private EcsPool<Comps.Projectiles> _projectilesPool;

      private EcsPool<ProjectilesSpawnOrigins> _shootOriginsPool;
      private EcsPool<SpreadAngle>             _spreadAnglePool;
      private EcsWorld                         _world;

      public ShootSys(DiContainer diContainer) {
         _diContainer = diContainer;
      } // *****************


      public void Init(IEcsSystems systems) {
         _world = systems.GetWorld();
         _filter = _world
                  .Filter<_base.Weapon>()
                  .Inc<ProjectilesSpawnOrigins>()
                  .Inc<Comps.Projectiles>()
                  .Inc<WantAttack>()
                  .Exc<BlockAttack>()
                  .End();

         _shootOriginsPool = _world.GetPool<ProjectilesSpawnOrigins>();
         _projectilesPool  = _world.GetPool<Comps.Projectiles>();

         _displacementPool = _world.GetPool<EcsTransform>();
         _isShootingPool   = _world.GetPool<IsAttacking>();
         _spreadAnglePool  = _world.GetPool<SpreadAngle>();

         _insContainer = new GameObject("Projectiles Container").transform;
      }

      public void Run(IEcsSystems systems) {
         foreach (int e in _filter) {
            ref List<ConvertToEntity> projectiles  = ref _projectilesPool.Get(e).value;
            ref List<EcsTransform>    shootOrigins = ref _shootOriginsPool.Get(e).value;

            ConvertToEntity projectile   = projectiles[Random.Range(0, projectiles.Count - 1)];
            EcsTransform    displacement = _displacementPool.Has(e) ? _displacementPool.Get(e) : default;

            foreach (EcsTransform shootOrigin in shootOrigins.Select(origin => origin.WithParent(displacement))) {
               EcsTransform spawn = shootOrigin;

               if (_spreadAnglePool.Has(e)) {
                  Vector3 angles = spawn.EulerAngles;
                  angles.z          += _spreadAnglePool.Get(e).angle.GetRandom();
                  spawn.EulerAngles =  angles;
               }

               // _diContainer
               //  .InstantiatePrefab(projectile, _insContainer)
               //  .GetComponent<ConvertToEntity>()

               //Transform insT = projectileInstance.transform;
               //insT.position = insDisp.position;
               //insT.rotation = insDisp.rotation;
               //;

               Object.Instantiate(projectile, spawn.position, spawn.rotation);
               
               
            }

            _isShootingPool.Add(e);
         }
      }
   }
}
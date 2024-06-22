using Events;
using Extentions;
using HitBoxes.Comps;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace HitBoxes.Sys {
  public class HitReceiveSys : IEcsRunSystem, IEcsInitSystem {
    private readonly EventsBus             _eventsBus;
    private readonly Collider2D[]          _results;
    private          EcsPool<EcsTransform> _displacementPool;
    private          EcsFilter             _filter;

    private EcsPool<HitBox> _hitBoxPool;
    private EcsWorld        _world;


    public HitReceiveSys(EventsBus eventsBus) {
      _eventsBus = eventsBus;
      _results   = new Collider2D[16];
    }

    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<HitBox>().End();

      _hitBoxPool       = _world.GetPool<HitBox>();
      _displacementPool = _world.GetPool<EcsTransform>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int de in _filter) {
        ref HitBox hitBox = ref _hitBoxPool.Get(de);
        Area       area   = hitBox.Area;
        var        angle  = 0f;

        if (_displacementPool.TryGet(de, out EcsTransform disp)) {
          area.origin += disp.position;
          angle       =  disp.EulerAngles.z;
        }

        int size = Physics2D.OverlapBoxNonAlloc(area.origin, area.size, angle, _results, hitBox.LayerMask);

        for (var i = 0; i < size; i++) {
          Collider2D collider = _results[i];

          if (!collider.gameObject.TryGetComponent(out ConvertToEntity convertToEntity))
            continue;
          if (!convertToEntity.Packed.Unpack(_world, out int te))
            continue;

          _eventsBus.NewEvent<HitEvent>() = new HitEvent {
            dealer = _world.PackEntityWithWorld(de), taker = _world.PackEntityWithWorld(te)
          };
        }
      }
    }
  }
}
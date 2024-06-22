using Camera.Shake.Comps;
using Events;
using Leopotam.EcsLite;
using UnityRef.Transform.Comp;
using UnityRef.Transform.Extentions.DOTween;

namespace Camera.Shake.Sys {
  public class ReceiveCameraShakeEventSys : IEcsRunSystem, IEcsInitSystem {
    private readonly EventsBus _eventsBus;
    private          EcsFilter _camerafilter;

    private EcsPool<EcsTransform> _displacementPool;

    private EcsWorld                    _ecsWorld;
    private EcsPool<PlayingCameraShake> _playingCameraShakePool;


    public ReceiveCameraShakeEventSys(EventsBus eventsBus) {
      _eventsBus = eventsBus;
    }

    public void Init(IEcsSystems systems) {
      _ecsWorld     = systems.GetWorld();
      _camerafilter = _ecsWorld.Filter<CameraTag>().Inc<EcsTransform>().Exc<PlayingCameraShake>().End();

      _displacementPool       = _ecsWorld.GetPool<EcsTransform>();
      _playingCameraShakePool = _ecsWorld.GetPool<PlayingCameraShake>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int ev in _eventsBus.GetEventBodies(out EcsPool<CameraShakeEvent> cameraShakeEventPoll)) {
        ref CameraShakeEvent shakeEvent = ref cameraShakeEventPoll.Get(ev);

        foreach (int cameraE in _camerafilter) {
          ref PlayingCameraShake playingShake = ref _playingCameraShakePool.Add(cameraE);

          playingShake.shakeEvent = shakeEvent;
          playingShake.posTween = _displacementPool.GetShakePositionTween(
            cameraE,
            shakeEvent.duration,
            shakeEvent.posStrength
          );
          playingShake.rotTween = _displacementPool.GetShakeRotationTween(
            cameraE,
            shakeEvent.duration,
            shakeEvent.rotStrength
          );
        }

        cameraShakeEventPoll.Del(ev);
      }
    }
  }
}
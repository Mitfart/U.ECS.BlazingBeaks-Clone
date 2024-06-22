using Camera.Shake.Comps;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;
using UnityRef.Transform.Comp;

namespace Camera.Shake.Sys {
  public class ShakeCameraSys : IEcsRunSystem, IEcsInitSystem {
    private EcsFilter _filter;

    private EcsPool<PlayingCameraShake> _playingCameraShakePool;
    private EcsWorld                    _world;


    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<CameraTag>().Inc<PlayingCameraShake>().Inc<EcsTransform>().End();

      _playingCameraShakePool = _world.GetPool<PlayingCameraShake>();
    }

    public void Run(IEcsSystems systems) {
      float delta         = Time.deltaTime;
      float unscaledDelta = Time.unscaledDeltaTime;

      foreach (int e in _filter) {
        ref PlayingCameraShake shake = ref _playingCameraShakePool.Get(e);

        shake.posTween.ManualUpdate(delta, unscaledDelta);
        shake.rotTween.ManualUpdate(delta, unscaledDelta);

        if (!shake.rotTween.IsComplete()
         || !shake.posTween.IsComplete())
          continue;

        shake.rotTween.Kill();
        shake.posTween.Kill();
        _playingCameraShakePool.Del(e);
      }
    }
  }
}
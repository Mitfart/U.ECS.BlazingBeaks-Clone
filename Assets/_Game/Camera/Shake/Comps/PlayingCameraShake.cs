using System;
using DG.Tweening;

namespace Camera.Shake.Comps {
  [Serializable]
  public struct PlayingCameraShake {
    public CameraShakeEvent shakeEvent;

    public Tween posTween;
    public Tween rotTween;
  }
}
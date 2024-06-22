using System;
using Events;
using UnityEngine;

namespace Camera.Shake.Comps {
  [Serializable]
  public struct CameraShakeEvent : IEvent {
    public float   duration;
    public Vector3 posStrength;
    public Vector3 rotStrength;
  }
}
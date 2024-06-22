using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace Camera.Shake.Comps {
  [DisallowMultipleComponent] public class CameraTagProv : EcsProvider<CameraTag> { }

  [Serializable]
 
  public struct CameraTag { }
}
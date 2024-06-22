using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using Unity.VisualScripting;
using UnityEngine;

namespace Extentions.ConvertExtensions {
  public class ConvertedProv : EcsProvider<Converted> {
    private void OnEnable() {
      if (component.gameObject.IsUnityNull())
        component.gameObject = gameObject;
    }
  }

  [Serializable]
  public struct Converted {
    public GameObject gameObject;
  }
}
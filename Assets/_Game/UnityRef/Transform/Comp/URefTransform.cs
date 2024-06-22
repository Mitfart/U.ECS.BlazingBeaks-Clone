using System;
using Extentions.ConvertExtensions.Providers;
using UnityEngine;

namespace UnityRef.Transform.Comp {
  [Serializable]

  public struct URefTransform : IEcsURef<UnityEngine.Transform> {
    [field: SerializeField] public UnityEngine.Transform Value { get; set; }
  }
}
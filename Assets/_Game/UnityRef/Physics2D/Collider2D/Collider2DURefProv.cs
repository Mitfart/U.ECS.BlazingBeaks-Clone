using System;
using Extentions.ConvertExtensions.Providers;
using UnityEngine;

namespace UnityRef.Physics2D.Collider2D {
  [DisallowMultipleComponent]
  [RequireComponent(typeof(UnityEngine.Collider2D))]
  public class Collider2DURefProv : EcsUnityURefProv<UnityEngine.Collider2D, Collider2DRef> { }

  [Serializable]

  public struct Collider2DRef : IEcsURef<UnityEngine.Collider2D> {
    [field: SerializeField] public UnityEngine.Collider2D Value { get; set; }
  }
}
using System;
using Extentions.ConvertExtensions.Providers;
using UnityEngine;

namespace UnityRef.Physics2D.Rigidbody2D {
  [DisallowMultipleComponent]
  [RequireComponent(typeof(UnityEngine.Rigidbody2D))]
  public class Rigidbody2DURefProv : EcsUnityURefProv<UnityEngine.Rigidbody2D, Rigidbody2DRef> { }

  [Serializable]

  public struct Rigidbody2DRef : IEcsURef<UnityEngine.Rigidbody2D> {
    [field: SerializeField] public UnityEngine.Rigidbody2D Value { get; set; }
  }
}
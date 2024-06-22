using System;
using Extentions.ConvertExtensions.Providers;
using UnityEngine;

namespace UnityRef.Animator {
  [DisallowMultipleComponent]
  [RequireComponent(typeof(UnityEngine.Animator))]
  public class AnimatorURefProv : EcsUnityURefProv<UnityEngine.Animator, AnimatorURef> { }

  [Serializable]

  public struct AnimatorURef : IEcsURef<UnityEngine.Animator> {
    [field: SerializeField] public UnityEngine.Animator Value { get; set; }
  }
}
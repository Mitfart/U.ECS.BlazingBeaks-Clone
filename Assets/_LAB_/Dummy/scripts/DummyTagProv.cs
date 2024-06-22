using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

namespace _TEST {
  [DisallowMultipleComponent] public class DummyTagProv : EcsProvider<DummyTag> { }

  [Serializable] public struct DummyTag { }
}
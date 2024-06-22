using System;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

[DisallowMultipleComponent] public class DamageProv : EcsProvider<Damage> { }

[Serializable]

public struct Damage {
  public float value;
}
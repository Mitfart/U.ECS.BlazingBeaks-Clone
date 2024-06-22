using System;
using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo.Providers;
using UnityEngine;

[DisallowMultipleComponent]
public class HealthProv : BaseEcsProvider {
  public int health;
  public int maxHealth;

  public override void Convert(int e, EcsWorld world) {
    world.GetPool<MaxHealth>().Set(e).value = maxHealth;
    world.GetPool<Health>().Set(e).value    = health;

    Destroy(this);
  }

  private void OnValidate() {
    if (health > maxHealth)
      maxHealth = health;

    if (health <= 0)
      health = maxHealth;
  }
}

[Serializable]

public struct Health {
  public float value;
}

[Serializable]

public struct MaxHealth {
  public float value;
}
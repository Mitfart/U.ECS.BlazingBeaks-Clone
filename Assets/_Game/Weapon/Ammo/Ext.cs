using Weapon.Ammo._base.Comps;

namespace Weapon.Ammo {
  public static class Ext {
    public static bool IsFull(this Magazine magazine) {
      return magazine.amount == magazine.size;
    }

    public static bool IsEmpty(this Magazine magazine) {
      return magazine.amount <= 0;
    }

    public static bool IsFull(this _base.Comps.Ammo ammo) {
      return ammo.amount == ammo.size;
    }

    public static bool IsEmpty(this _base.Comps.Ammo ammo) {
      return ammo.amount <= 0;
    }
  }
}
using Destroy.Systems;
using Extentions;
using Zenject;

namespace Destroy {
  public class DestroySystems : EcsSystemsPack {
    public DestroySystems(DiContainer container) : base(container) {
      Add<LifeTimeDestroySys>();
      Add<DestroyIfHealthBelowZero>();
      Add<TestDestroySys>();
    }
  }
}
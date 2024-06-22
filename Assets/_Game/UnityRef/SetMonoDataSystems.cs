using Extentions;
using UnityRef.Transform.Sys;
using Zenject;

namespace UnityRef {
  public class SetMonoDataSystems : EcsSystemsPack {
    public SetMonoDataSystems(DiContainer container) : base(container) {
      Add<SetTransformSys>();
    }
  }
}
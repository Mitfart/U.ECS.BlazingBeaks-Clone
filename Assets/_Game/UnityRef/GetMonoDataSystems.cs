using Extentions;
using UnityRef.Transform.Sys;
using Zenject;

namespace UnityRef {
  public class GetMonoDataSystems : EcsSystemsPack {
    public GetMonoDataSystems(DiContainer container) : base(container) {
      Add<GetTransformSys>();
    }
  }
}
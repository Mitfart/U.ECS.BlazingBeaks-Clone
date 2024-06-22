using Camera.Shake.Sys;
using Extentions;
using Zenject;

namespace Camera.Shake {
  public class CameraShakeSystems : EcsSystemsPack {
    public CameraShakeSystems(DiContainer container) : base(container) {
      Add<ReceiveCameraShakeEventSys>(); // *************************
      Add<ShakeCameraSys>();             // *************************
    }
  }
}
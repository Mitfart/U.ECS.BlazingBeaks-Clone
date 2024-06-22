using Leopotam.EcsLite;

namespace Behavior.Nodes.Debug {
  public class LogNode : EcsBehNode {
    private readonly string _massage;



    public LogNode(string massage, params EcsBehNode[] childNodes) : base(childNodes) {
      _massage = massage;
    }

    protected override void OnBegin(int e, EcsWorld world) {
#if UNITY_EDITOR
      UnityEngine.Debug.Log($"{_massage}__({e})_({world})");
#endif
    }
  }
}
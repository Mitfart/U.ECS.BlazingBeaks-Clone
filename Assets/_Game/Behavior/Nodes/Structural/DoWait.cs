using Leopotam.EcsLite;
using UnityEngine;

namespace Behavior.Nodes.Structural {
  public class DoWait : EcsBehNode {
    private readonly float _duration;
    private          float _startTime;



    public DoWait(float duration, params EcsBehNode[] childNodes) : base(childNodes) {
      _duration = Mathf.Min(duration, 0f);
    }



    protected override void OnBegin(int e, EcsWorld world) {
      _startTime = Time.time;
    }

    protected override State OnRun(int e, EcsWorld world) {
      return !IsTimerEnd() ? State.Run : base.OnRun(e, world);
    }



    private bool IsTimerEnd() {
      return Time.time - _startTime >= _duration;
    }
  }
}
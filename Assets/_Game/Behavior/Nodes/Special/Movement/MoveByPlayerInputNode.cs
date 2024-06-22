using Leopotam.EcsLite;
using Movement.Comps;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behavior.Nodes.Special.Movement {
  public class MoveByPlayerInputNode : EcsBehNode {
    private EcsFilter _filter;

    private EcsPool<MoveDirection> _moveDirectionPool;



    public MoveByPlayerInputNode(params EcsBehNode[] childNodes) : base(childNodes) { }



    protected override void OnBegin(int e, EcsWorld world) {
      _filter ??= world.Filter<MoveDirection>().End();

      _moveDirectionPool ??= world.GetPool<MoveDirection>();
    }

    protected override State OnRun(int e, EcsWorld world) {
      if (_moveDirectionPool.Has(e))
        _moveDirectionPool.Get(e).value = GetMovementInput().normalized;

      return base.OnRun(e, world);
    }

    private static Vector2 GetMovementInput() {
      Keyboard kb    = Keyboard.current;
      Vector2  input = Vector2.zero;

      if (kb.wKey.isPressed)
        input.y += 1f;
      if (kb.sKey.isPressed)
        input.y -= 1f;
      if (kb.dKey.isPressed)
        input.x += 1f;
      if (kb.aKey.isPressed)
        input.x -= 1f;
      return input;
    }
  }
}
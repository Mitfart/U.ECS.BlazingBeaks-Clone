
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Debug.Gizmos {
  public class GizmosService : MonoInstaller<GizmosService> {
#if UNITY_EDITOR
    private HashSet<Action> _prevToDraw;
    private HashSet<Action> _toDraw;

    public bool IsActive { get; private set; }
    public bool IsPaused { get; private set; }


#endif

    public override void InstallBindings() {
      Container
       .Bind<GizmosService>()
       .FromInstance(this);
    }


    public void Draw(Action draw) {
#if UNITY_EDITOR
      if (!IsActive)
        return;

      _toDraw.Add(draw);
#endif
    }


#if UNITY_EDITOR

    public void On() {
      if (IsActive)
        return;

      IsActive    = true;
      _toDraw     = new HashSet<Action>();
      _prevToDraw = new HashSet<Action>();
    }

    public void Off() {
      if (!IsActive)
        return;

      IsActive = false;
      _toDraw.Clear();
      _toDraw = null;
      _prevToDraw.Clear();
      _prevToDraw = null;
    }

    public void Toggle() {
      if (IsActive)
        Off();
      else
        On();
    }



    private void OnDrawGizmos() {
      if (!IsActive)
        return;

      if (IsPaused)
        (_prevToDraw, _toDraw) = (_toDraw, _prevToDraw);

      foreach (Action drawAction in _toDraw) {
        drawAction.Invoke();
        UnityEngine.Gizmos.color  = Color.white;
        UnityEngine.Gizmos.matrix = Matrix4x4.identity;
      }

      (_prevToDraw, _toDraw) = (_toDraw, _prevToDraw);
      _toDraw.Clear();
      IsPaused = true;
    }

    private void LateUpdate() {
      // hack: only knowing way to check for pause in the same frame
      IsPaused = false;
    }

    private void OnEnable() {
      On();
    }

    private void OnDisable() {
      Off();
    }
#endif
  }
}
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastructure {
  [DefaultExecutionOrder(-1)]
  [RequireComponent(typeof(SceneContext))]
  public class Bootstrap : MonoBehaviour {
    private IGameStateMachine _gameStateMachine;


    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) {
      _gameStateMachine = gameStateMachine;
    }

    private void Awake() {
      DontDestroyOnLoad(this);
    }

    private void Start() {
      _gameStateMachine.Enter<BootstrapState>();
    }

    private void Update() {
      _gameStateMachine.Tick();
    }

    private void FixedUpdate() {
      _gameStateMachine.FixedTick();
    }
  }
}
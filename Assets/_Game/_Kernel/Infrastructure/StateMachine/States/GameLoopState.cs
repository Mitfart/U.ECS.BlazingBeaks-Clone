using Engine;

namespace Infrastructure.StateMachine.States {
  public class GameLoopState : GameState, ITickable, IFixedTickable {
    private readonly Controls _controls;
    private readonly IEngine  _engine;



    public GameLoopState(
      IEngine  engine,
      Controls controls
    ) {
      _engine   = engine;
      _controls = controls;
    }


    public void FixedTick() {
      _engine.FixedTick();
    }

    public void Tick() {
      _engine.Tick();
    }



    public override void Enter() {
      _engine.Init();
      _controls.Gameplay.Enable();
    }

    public override void Exit() {
      _controls.Gameplay.Disable();
      _engine.Dispose();
    }



    private void EndSession() {
      StateMachine.Enter<FinishSessionState>();
    }
  }
}
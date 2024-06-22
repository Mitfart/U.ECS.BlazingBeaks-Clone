namespace Infrastructure.StateMachine.States {
  public class FinishSessionState : GameState /* ,IDataRequireState<???> */ {
    public override void Enter() {
      UnityEngine.Debug.Log("Finish!");
      StateMachine.Enter<LoadLevelState, string>(Scenes.MAIN);
    }
  }
}
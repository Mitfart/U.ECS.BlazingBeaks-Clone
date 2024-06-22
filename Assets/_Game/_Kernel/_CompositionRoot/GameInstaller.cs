using Infrastructure.Loading;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

public class GameInstaller : MonoInstaller {
  public override void InstallBindings() {
    BindSceneLoader();

    BindInputControls();

    BindEngine();

    BindGameStateMachine();
  }



  private void BindSceneLoader() {
    Container
     .Bind<ISceneLoader>()
     .To<SceneLoader>()
     .AsSingle();
  }

  private void BindInputControls() {
    Container
     .Bind<Controls>()
     .AsSingle();
  }

  private void BindEngine() {
    EcsEngineInstaller.Install(Container);
  }

  private void BindGameStateMachine() {
    Container.Bind<BootstrapState>().AsSingle();
    Container.Bind<LoadLevelState>().AsSingle();
    Container.Bind<GameLoopState>().AsSingle();
    Container.Bind<FinishSessionState>().AsSingle();

    Container
     .Bind<IGameStateMachine>()
     .To<GameStateMachine>()
     .AsSingle();
  }
}
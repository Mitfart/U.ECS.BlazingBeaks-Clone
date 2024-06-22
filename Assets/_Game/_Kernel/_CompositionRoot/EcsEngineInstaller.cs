using Engine;
using Events;
using Zenject;

public static class EcsEngineInstaller {
  public static void Install(DiContainer idiAdapter) {
    BindEvents(idiAdapter);
    BindSystems(idiAdapter);
    BindEngine(idiAdapter);
  }



  private static void BindEvents(DiContainer container) {
    container.BindInstance(new EventsBus()).AsSingle();
  }

  private static void BindSystems(DiContainer container) {
    container
     .Bind<MainSystemsPack>()
     .AsSingle();
  }

  private static void BindEngine(DiContainer container) {
    container
     .Bind<IEngine>()
     .To<EcsEngine>()
     .AsSingle();
  }
}
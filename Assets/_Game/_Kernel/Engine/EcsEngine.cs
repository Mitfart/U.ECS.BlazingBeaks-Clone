using Extentions;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UniLeo;

namespace Engine {
  public sealed class EcsEngine : IEngine {
    private readonly EscExtendedSystems _systems;
    private readonly MainSystemsPack    _systemsPack;
    private readonly EcsWorld           _world;



    public EcsEngine(MainSystemsPack systemsPack) {
      _systemsPack = systemsPack;

      _world   = new EcsWorld();
      _systems = new EscExtendedSystems(_world);

      SetupSystems();
      _systems.Add(new ConvertSceneSys());
      EcsWorldsLocator.RegisterAllFrom(_systems);
    }


    public void Init()      => _systems.Init();
    public void Tick()      => _systems.Run();
    public void FixedTick() => _systems.FixedRun();
    public void Dispose()   => _systems.Destroy();



    private void SetupSystems() {
      foreach (IEcsSystem system in _systemsPack.Systems)
        _systems.Add(system);
    }
  }
}
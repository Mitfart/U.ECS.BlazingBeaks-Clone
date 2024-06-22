using Leopotam.EcsLite;

namespace Behavior.ECS {
  public class ProcessBehaviorSys : IEcsRunSystem, IEcsInitSystem {
    private EcsPool<Behavior> _behaviorPool;
    private EcsFilter         _filter;
    private EcsWorld          _world;



    public void Init(IEcsSystems systems) {
      _world  = systems.GetWorld();
      _filter = _world.Filter<Behavior>().End();

      _behaviorPool = _world.GetPool<Behavior>();
    }

    public void Run(IEcsSystems systems) {
      foreach (int e in _filter)
        _behaviorPool
         .Get(e)
         .tree
         .Run(e, _world);
    }
  }
}
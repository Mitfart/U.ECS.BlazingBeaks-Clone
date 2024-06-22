using System.Collections.Generic;
using Leopotam.EcsLite;
using Zenject;

namespace Extentions {
  public abstract class EcsSystemsPack {
    private readonly DiContainer      _container;
    public           List<IEcsSystem> Systems { get; }


    public EcsSystemsPack(DiContainer container) {
      _container = container;
      Systems    = new List<IEcsSystem>();
    }


    protected void AddPack<TPack>() where TPack : EcsSystemsPack {
      _container
       .Bind<TPack>()
       .AsTransient();

      Systems.AddRange(_container.Resolve<TPack>().Systems);
    }

    protected void Add<TSystem>() where TSystem : class, IEcsSystem {
      _container
       .Bind<TSystem>()
       .AsTransient();

      Systems.Add(_container.Resolve<TSystem>());
    }

    protected void AddByInstance<TSystem>(TSystem instance) where TSystem : class, IEcsSystem {
      _container
       .BindInstance(instance)
       .AsTransient();

      Systems.Add(instance);
    }
  }
}
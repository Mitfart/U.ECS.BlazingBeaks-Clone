using System;

namespace Engine {
  public interface IEngine : ITickable, IFixedTickable, IDisposable, IInitable { }
}
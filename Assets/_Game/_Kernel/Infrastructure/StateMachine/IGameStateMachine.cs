using Engine;

namespace Infrastructure.StateMachine {
  public interface IGameStateMachine : IStateMachine<IGameState>, ITickable, IFixedTickable { }

  public interface IGameState : IState {
    public IGameStateMachine StateMachine { get; }

    public void Init(IGameStateMachine stateMachine);
  }
}
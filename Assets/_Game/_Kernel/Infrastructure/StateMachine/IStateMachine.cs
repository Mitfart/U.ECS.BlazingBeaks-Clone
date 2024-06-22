namespace Infrastructure.StateMachine {
  public interface IStateMachine<in TStateContract> where TStateContract : IState {
    void Enter<TState>() where TState : class, TStateContract;

    void Enter<TState, TData>(TData data) where TState : class, TStateContract, IDataRequireState<TData>;
  }

  public interface IState {
    void Enter();
    void Exit();
  }

  public interface IDataRequireState<in T> : IState {
    void SetData(T data);
  }
}
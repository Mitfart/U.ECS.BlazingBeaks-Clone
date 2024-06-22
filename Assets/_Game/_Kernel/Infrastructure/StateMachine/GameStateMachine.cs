using System;
using System.Collections.Generic;
using Engine;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine {
  public class GameStateMachine : IGameStateMachine {
    private readonly Dictionary<Type, IGameState> _registeredStates;

    private IGameState _currentState;



    public GameStateMachine(
      BootstrapState     bootstrap,
      LoadLevelState     loadLevel,
      GameLoopState      gameLoop,
      FinishSessionState finishSession
    ) {
      _registeredStates = new Dictionary<Type, IGameState>();

      RegisterState(bootstrap);
      RegisterState(loadLevel);
      RegisterState(gameLoop);
      RegisterState(finishSession);
    }



    public void Tick() {
      if (_currentState is ITickable state)
        state.Tick();
    }

    public void FixedTick() {
      if (_currentState is IFixedTickable state)
        state.FixedTick();
    }


    public void Enter<TState>()
      where TState : class, IGameState {
      ChangeState<TState>().Enter();
    }

    public void Enter<TState, TData>(TData data)
      where TState : class, IGameState, IDataRequireState<TData> {
      TState newState = ChangeState<TState>();
      newState.SetData(data);
      newState.Enter();
    }



    private void RegisterState<TState>(TState state) where TState : IGameState {
      state.Init(this);
      _registeredStates.Add(typeof(TState), state);
    }

    private TState ChangeState<TState>() where TState : class, IGameState {
      _currentState?.Exit();

      TState state = GetState<TState>();
      _currentState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IGameState {
      return (TState) _registeredStates[typeof(TState)];
    }
  }
}
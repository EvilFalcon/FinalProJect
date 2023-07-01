using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void SetDefaultState<TState>() where TState : class, IState
        {
            _stateMachine.Enter<TState>();
        }
    }
}
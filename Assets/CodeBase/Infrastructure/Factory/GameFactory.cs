using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory
    {
        private readonly GameStateMachine _stateMachine;

        public GameFactory(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public Game Create() =>
            new Game(_stateMachine);
    }
}
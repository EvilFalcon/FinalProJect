using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Factory
{
    public class StateMachineFactory
    {
        private readonly StateFactory _stateFactory;

        public StateMachineFactory(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public GameStateMachine Create()
        {
            return new GameStateMachine(_stateFactory);
        }
    }
}
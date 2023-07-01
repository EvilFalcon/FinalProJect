using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            GameBuilder gameBuilder = new GameBuilder(this);

            _game = gameBuilder.Bild();
            _game.SetDefaultState<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
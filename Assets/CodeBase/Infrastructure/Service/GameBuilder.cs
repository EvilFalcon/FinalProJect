using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services.Input;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public class GameBuilder
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public GameBuilder(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public Game Bild()
        {
            LoadingCurtainFactory curtainFactory = new LoadingCurtainFactory();
            HeroInputService heroInputService = new HeroInputService();
            HeroFactory heroFactory = new HeroFactory(heroInputService);
            HudFactory hudFactory = new HudFactory();

            PersistentProgress.PersistentProgress persistentProgress = new PersistentProgress.PersistentProgress();
            RepositorySaveLoadComponent repositorySaveLoadComponent = new RepositorySaveLoadComponent();
            SceneLoader sceneLoader = new SceneLoader(_coroutineRunner);

            LoadingCurtain curtain = curtainFactory.Create();
            SaveLoadService saveLoadService = new SaveLoadService(persistentProgress, repositorySaveLoadComponent);
            StaticDataService staticDataService = new StaticDataService();
            MonsterFactory monsterFactory = new MonsterFactory(staticDataService, heroFactory);
            StateFactory stateFactory = new StateFactory(sceneLoader, curtain, heroFactory, hudFactory, persistentProgress, saveLoadService, repositorySaveLoadComponent,monsterFactory);
            StateMachineFactory stateMachineFactory = new StateMachineFactory(stateFactory);

            GameStateMachine stateMachine = stateMachineFactory.Create();
            GameFactory gameFactory = new GameFactory(stateMachine);
            return gameFactory.Create();
        }
    }
}
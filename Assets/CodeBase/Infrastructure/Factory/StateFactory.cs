using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Factory
{
    public class StateFactory
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly HeroFactory _heroFactory;
        private readonly HudFactory _hudFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly SaveLoadService _saveLoadService;
        private readonly RepositorySaveLoadComponent _repositorySaveLoadComponent;
        private readonly MonsterFactory _monsterFactory;

        public StateFactory(SceneLoader sceneLoader,
            LoadingCurtain curtain,
            HeroFactory heroFactory,
            HudFactory hudFactory,
            IPersistentProgressService persistentProgress,
            SaveLoadService saveLoadService,
            RepositorySaveLoadComponent repositorySaveLoadComponent,
            MonsterFactory monsterFactory)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _heroFactory = heroFactory;
            _hudFactory = hudFactory;
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
            _repositorySaveLoadComponent = repositorySaveLoadComponent;
            _monsterFactory = monsterFactory;
        }

        public Dictionary<Type, IExitableState> Create(GameStateMachine gameStateMachine)
        {
            return new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(
                    gameStateMachine,
                    _sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    gameStateMachine,
                    _sceneLoader,
                    _curtain,
                    _heroFactory,
                    _hudFactory,
                    _repositorySaveLoadComponent,
                    _persistentProgress,
                    _saveLoadService,
                    _monsterFactory),
                [typeof(GameLoopState)] = new GameLoopState(
                    gameStateMachine),
                [typeof(LoadProgresState)] = new LoadProgresState(
                    gameStateMachine,
                    _persistentProgress,
                    _saveLoadService),
            };
        }
    }
}
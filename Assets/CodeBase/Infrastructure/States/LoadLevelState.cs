using System.Linq;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly HeroFactory _heroFactory;
        private readonly HudFactory _hudFactory;
        private readonly RepositorySaveLoadComponent _repositorySaveLoadComponent;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly SaveLoadService _saveLoadService;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            HeroFactory heroFactory,
            HudFactory hudFactory,
            RepositorySaveLoadComponent repositorySaveLoadComponent,
            IPersistentProgressService persistentProgress, SaveLoadService saveLoadService
        )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _heroFactory = heroFactory;
            _hudFactory = hudFactory;
            _repositorySaveLoadComponent = repositorySaveLoadComponent;
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgresReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgresReaders()
        {
            _repositorySaveLoadComponent.Update();

            foreach (ISavedProgressReader progressReader in _repositorySaveLoadComponent.ProgressReaders)
            {
                progressReader.LoadProgres(_persistentProgress.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            var initialPoint = Object.FindObjectOfType<PlayerInitialPoint>();
            
            var saveTriggers = Object.FindObjectsOfType<SaveTrigger>().ToList();
            
            saveTriggers.ForEach(triggers => triggers.Init(_saveLoadService));
            
            GameObject hero = _heroFactory.Create(initialPoint.gameObject.transform);
            
            _repositorySaveLoadComponent.AddGameObject(hero);
            
            _hudFactory.Create();
            
            CameraFollow(hero);
            
            hero.SetActive(true);
        }

        private void CameraFollow(GameObject hero)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}
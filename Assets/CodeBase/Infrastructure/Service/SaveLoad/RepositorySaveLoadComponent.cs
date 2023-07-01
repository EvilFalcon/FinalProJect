using System.Collections.Generic;
using CodeBase.Infrastructure.Service.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.SaveLoad
{
    public class RepositorySaveLoadComponent
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private List<ISavedProgressReader> _progressReaders = new List<ISavedProgressReader>();
        private List<ISavedProgress> _progressWriters = new List<ISavedProgress>();

        public ISavedProgressReader[] ProgressReaders => _progressReaders.ToArray();
        public ISavedProgress[] ProgressWriters => _progressWriters.ToArray();

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
            _gameObjects.Clear();
        }

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Update()
        {
            foreach (var gameObject in _gameObjects)
            {
                if (gameObject != null)
                    GetSaveLoadComponent(gameObject);
            }
        }

        private void GetSaveLoadComponent(GameObject gameObject)
        {
            foreach (ISavedProgressReader component in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(component);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriters)
                _progressWriters.Add(progressWriters);

            _progressReaders.Add(progressReader);
        }
    }
}
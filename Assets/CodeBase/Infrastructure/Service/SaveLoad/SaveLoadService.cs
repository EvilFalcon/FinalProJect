using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly PersistentProgress.PersistentProgress _progress;
        private readonly RepositorySaveLoadComponent _repositorySaveLoadComponent;

        public SaveLoadService(
            PersistentProgress.PersistentProgress progress,
            RepositorySaveLoadComponent repositorySaveLoadComponent
        )
        {
            _progress = progress;
            _repositorySaveLoadComponent = repositorySaveLoadComponent;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _repositorySaveLoadComponent.ProgressWriters)
                progressWriter.UpdateProgress(_progress.PlayerProgress);

            PlayerPrefs.SetString(Constants.PlayerPrefsKeyProgress, _progress.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(Constants.PlayerPrefsKeyProgress)?.ToDeserialized<PlayerProgress>();

        public void UpdateRepository()
        {
            _repositorySaveLoadComponent.Update();
        }
    }
}
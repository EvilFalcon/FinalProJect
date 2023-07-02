using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgresState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progress;
        private readonly ISaveLoadService _saveLoadedProgress;

        public LoadProgresState(
            GameStateMachine gameStateMachine,
            IPersistentProgressService progress,
            ISaveLoadService saveLoadService
        )
        {
            _gameStateMachine = gameStateMachine;
            _progress = progress;
            _saveLoadedProgress = saveLoadService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            LoadProgressOrInit();
            _gameStateMachine.Enter<LoadLevelState, string>(_progress.PlayerProgress.WorldData.PlayerPositionOnLevel.Lavel);
        }

        private void LoadProgressOrInit()
        {
            _progress.PlayerProgress = _saveLoadedProgress.LoadProgress() ?? CreateProgress();
        }

        private PlayerProgress CreateProgress()
        {
            PlayerProgress progress = new PlayerProgress(Constants.InitialLevel);
            progress.HeroState.MaxHP = 50;
            progress.HeroStats.Damage = 1;
            progress.HeroStats.DamageRadius = 0.5f;
            progress.HeroState.ResetHp();
            return progress;
        }
    }
}
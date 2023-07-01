using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgres(PlayerProgress playerProgress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}
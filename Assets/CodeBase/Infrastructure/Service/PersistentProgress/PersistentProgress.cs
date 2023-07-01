using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.PersistentProgress
{
    public class PersistentProgress : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
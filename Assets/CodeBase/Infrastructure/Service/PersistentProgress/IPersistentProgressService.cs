using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}
using CodeBase.Logic;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameObjectFactory
    {
        public IHeroTransform Instantiate { get; }
    }
}
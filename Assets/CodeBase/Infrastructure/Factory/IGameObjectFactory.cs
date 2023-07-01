using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameObjectFactory
    {
        public GameObject Instantiate { get; }
    }
}
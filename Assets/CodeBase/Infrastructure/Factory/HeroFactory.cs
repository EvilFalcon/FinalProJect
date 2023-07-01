using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IGameObjectFactory
    {
        public GameObject Instantiate { get; private set; }

        public GameObject Create(Transform transform)
        {
            GameObject hero = Object.Instantiate(Resources.Load<GameObject>(AssetPath.HeroPath), transform.position, Quaternion.identity);
            Instantiate = hero;
            return hero;
        }
    }
}
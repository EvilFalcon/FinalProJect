using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class Follow : MonoBehaviour
    {
        public abstract void InitializeHeroTransform(Transform heroTransform);
    }
}
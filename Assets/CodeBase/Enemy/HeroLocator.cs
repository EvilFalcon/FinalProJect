using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class HeroLocator : MonoBehaviour
    {
        public event Action FoundHero;
        public Transform HeroTransform { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out global::Hero hero))
            {
                HeroTransform = hero.transform;
                FoundHero?.Invoke();
            }
        }
    }
}
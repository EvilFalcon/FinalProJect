using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<global::Hero> TriggerEnter;
        public event Action<global::Hero> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out global::Hero hero))
                TriggerEnter?.Invoke(hero);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out global::Hero hero))
            {
                TriggerExit?.Invoke(hero);
            }
        }
    }
}
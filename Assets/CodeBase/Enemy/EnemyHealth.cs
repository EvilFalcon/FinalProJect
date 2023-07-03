using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IEnemyHealth
    {
        [SerializeField] private EnemyAnimator _animator;
         private float _current;
         private float _max;
        public float Current => _current;
        public float Max => _max;

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            _current -= damage;
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }

        public void SetValue(float value)
        {
            _max = value;
            _current = _max;
        }
    }
}
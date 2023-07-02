using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator _animator;

        private State _state;
        private float _current;
        private float _max;
        public float Current => _current;
        public float Max => _max;

        public event Action HealthChanged;

        public void LoadProgres(PlayerProgress playerProgress)
        {
            _state = playerProgress.HeroState;
            _current = playerProgress.HeroState.CurrentHP;
            _max = playerProgress.HeroState.MaxHP;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.CurrentHP = Current;
            playerProgress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (_current <= 0)
                return;

            _current -= damage;
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}
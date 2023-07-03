using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(
        typeof(EnemyHealth),
        typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private GameObject _deathFx;
        private Coroutine _coroutine;

        public event Action Happend;

        private void Start()
        {
            _enemyHealth.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            UnregisterHealthChangedHandler();
        }

        private void OnHealthChanged()
        {
            if (_enemyHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _animator.PlayDeath();
            SpawnDeathFx();
            _coroutine = StartCoroutine(DestroyTimer());
            Happend?.Invoke();
        }

        private void SpawnDeathFx()
        {
            UnregisterHealthChangedHandler();
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }

        private void UnregisterHealthChangedHandler()
        {
            _enemyHealth.HealthChanged -= OnHealthChanged;
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            
            StopCoroutine(_coroutine);
            Destroy(gameObject);
        }
    }
}
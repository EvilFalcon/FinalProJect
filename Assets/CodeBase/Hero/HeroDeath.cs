using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private GameObject DeathFx;
        private bool _isDeath;

        private void Start() =>
            _heroHealth.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _heroHealth.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDeath&&_heroHealth.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDeath = true;
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            _heroAnimator.PlayDeath();
            Instantiate(DeathFx, transform.position, Quaternion.identity);
        }
    }
}
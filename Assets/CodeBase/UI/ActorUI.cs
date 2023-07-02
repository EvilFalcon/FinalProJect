using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBarView;

        private IHealth _health;

        private void OnEnable()
        {
            if (_health == null)
                return;

            _health.HealthChanged += UpdateHpBar;
        }

        private void OnDisable()
        {
            if (_health == null)
                return;

            _health.HealthChanged -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            enabled = false;
            _health = health;
            enabled = true;
        }

        private void UpdateHpBar() =>
            _hpBarView.SetValue(_health.Current/ _health.Max);
    }
}
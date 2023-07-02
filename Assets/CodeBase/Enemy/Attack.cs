using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _cleavage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage = 10;

        private Transform _heroTransform;
        private EnemyAnimator _animator;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _animator = GetComponent<EnemyAnimator>();
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (CooldownIsUp() == false)
                _attackCooldown -= Time.deltaTime;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        public void Disable()
        {
            _attackIsActive = false;
        }

        public void Enable()
        {
            _attackIsActive = true;
        }

        public void Constructor(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private bool Hit(out Collider hit)
        {
            Vector3 startPoint = GetPointEffectiveDistance();
            int hitCount = Physics.OverlapSphereNonAlloc(startPoint, _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 GetPointEffectiveDistance()
        {
            Vector3 position;
            var transform1 = transform;
            return new Vector3((position = transform1.position).x, position.y + 0.5f, position.z) + transform1.forward * _effectiveDistance;
        }

        private void OnAttackEnded()
        {
            _attackCooldown = _cooldown;
            _isAttacking = false;
        }

        private bool CanAttack() =>
            _attackIsActive && CooldownIsUp() && !_isAttacking;

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;

        private void StartAttack()
        {
            _isAttacking = true;
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();
        }
    }
}
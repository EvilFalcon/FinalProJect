using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Logic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;
        private IInputService _inputService;
        private int _layerMask;
        private float _radius;
        private Collider[] _hits = new Collider[3];
        private float _damage;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && _animator.IsAttacking == false)
            {
                _animator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i]
                    .transform
                    .parent
                    .GetComponent<IHealth>()
                    .TakeDamage(_damage);
            }
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _radius, _hits, _layerMask);

        private Vector3 StartPoint()
        {
            var position = transform.position;
            return new Vector3(position.x, position.y / 2, position.z);
        }

        public void LoadProgres(PlayerProgress playerProgress)
        {
            _radius = playerProgress.HeroStats.DamageRadius;
            _damage = playerProgress.HeroStats.Damage;
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(
        typeof(EnemyAnimator),
        typeof(NavMeshAgent))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        private NavMeshAgent _meshAgent;
        private EnemyAnimator _enemyAnimator;

        private void Awake()
        {
            _meshAgent = GetComponent<NavMeshAgent>();
            _enemyAnimator = GetComponent<EnemyAnimator>();
        }

        private void Update()
        {
            if (ShouldMove())
                _enemyAnimator.Move(_meshAgent.velocity.magnitude);
            else
                _enemyAnimator.StopMoving();
        }

        private bool ShouldMove() =>
            _meshAgent.velocity.magnitude > MinimalVelocity && _meshAgent.remainingDistance > _meshAgent.radius;
    }
}
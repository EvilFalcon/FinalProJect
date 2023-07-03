using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(
        typeof(NavMeshAgent),
        typeof(AnimateAlongAgent))]
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1;
        private NavMeshAgent _agent;
        private Transform _transform;
        private Vector3 _spawnPoint;

        private void Update()
        {
            HeroNotReached();
        }

        private void HeroNotReached()
        {
            if (_transform == null)
                return;

            if (Vector3.Distance(_agent.transform.position, _transform.position) >= MinimalDistance)
                _agent.destination = _transform.position;
        }

        public override void InitializeHeroTransform(Transform heroTransform)
        {
            _transform = heroTransform;

            if (heroTransform == null)
                _agent.destination = _spawnPoint;
        }

        public void Construct(NavMeshAgent navMeshAgent)
        {
            _agent = navMeshAgent;
            _spawnPoint = transform.position;
        }
    }
}
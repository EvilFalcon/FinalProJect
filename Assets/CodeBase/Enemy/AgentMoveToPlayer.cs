using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;
        private NavMeshAgent _agent;
        private Transform _heroTransform;

        private void Update()
        {
            HeroNotReached();
        }

        private void HeroNotReached()
        {
            if (Vector3.Distance(_agent.transform.position, _heroTransform.position) <= MinimalDistance)
                _agent.destination = _heroTransform.position;
        }
    }
}
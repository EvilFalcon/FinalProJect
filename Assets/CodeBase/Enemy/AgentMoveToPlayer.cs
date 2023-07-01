using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(
        typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;
        private NavMeshAgent _agent;
        private Transform _heroTransform;
        private HeroLocator _locator;

        private void Awake()
        {
            _locator = GetComponentInChildren<HeroLocator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _locator.FoundHero += InitializeHeroTransform;
        }

        private void OnDisable()
        {
            _locator.FoundHero -= InitializeHeroTransform;
        }

        private void Update()
        {
            HeroNotReached();
        }

        private void HeroNotReached()
        {

            if (_heroTransform != null)
            {
            Debug.Log(Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance);
               // if (Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance)
               // {
                    Debug.Log("я в оп");
                    _agent.destination = _heroTransform.position;
                //}
            }
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _locator.transform;
        }
    }
}
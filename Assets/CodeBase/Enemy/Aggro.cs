using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private int _cooldown;
        [SerializeField] private TriggerObserver _triggerObserver;

        private AgentMoveToPlayer _follow;
        private Coroutine _aggroCoroutine;
        private bool _hesAggroTarget;

        private void Awake()
        {
            _follow = GetComponent<AgentMoveToPlayer>();
        }

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(global::Hero @object)
        {
            if (!_hesAggroTarget)
            {
                _hesAggroTarget = true;
                StopAggroCoroutine();

                SwitchFollowOn(@object);
            }
        }

        private void TriggerExit(global::Hero @object)
        {
            if (_hesAggroTarget)
            {
                _hesAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);

            SwitchFollowOff();
        }

        private void SwitchFollowOn(global::Hero @object)
        {
            _follow.enabled = true;
            _follow.InitializeHeroTransform(@object.transform);
        }

        private void SwitchFollowOff()
        {
            _follow.InitializeHeroTransform(default);
            _follow.enabled = false;
        }
    }
}
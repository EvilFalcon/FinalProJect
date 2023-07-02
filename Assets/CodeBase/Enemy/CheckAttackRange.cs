using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private Attack _attack;

        private void Awake()
        {
            _attack = GetComponent<Attack>();
        }

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            _attack.Disable();
        }

        private void TriggerExit(global::Hero @object)
        {
            _attack.Disable();
        }

        private void TriggerEnter(global::Hero @object)
        {
            _attack.Enable();
            _attack.Constructor(@object.transform);
        }
    }
}
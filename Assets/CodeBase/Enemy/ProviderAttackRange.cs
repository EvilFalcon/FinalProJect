using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class ProviderAttackRange : MonoBehaviour
    { 
        [SerializeField] private TriggerObserver _triggerObserver;
        private Attack _attack;

        private void Start()
        {
            _attack.Disable();
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        public void Constructor(Attack attack)
        {
            _attack = attack;
        }
        
        private void TriggerExit(global::Hero @object)
        {
            _attack.Disable();
        }

        private void TriggerEnter(global::Hero @object)
        {
            _attack.Enable();
        }
    }
}
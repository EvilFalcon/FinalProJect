using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField] private float _speed;

        private Transform _heroTransform;

        private Vector3 _positionToLook;

        private void Update()
        {
            if (IsInitialized())
                RotateTowardsHero();
        }

        public override void InitializeHeroTransform(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            var position = transform.position;
            Vector3 positionDelta = _heroTransform.position - position;
            _positionToLook = new Vector3(positionDelta.x, position.y, positionDelta.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() =>
            _speed * Time.deltaTime;

        private bool IsInitialized() =>
            _heroTransform != null;
    }
}
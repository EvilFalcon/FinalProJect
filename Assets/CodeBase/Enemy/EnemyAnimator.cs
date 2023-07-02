using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private readonly int _attack = Animator.StringToHash("Attack_1");
        private readonly int _speed = Animator.StringToHash("Speed");
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _hit = Animator.StringToHash("Hit");
        private readonly int _die = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator _animator;
        private AnimatorState _state;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(State);

        AnimatorState IAnimationStateReader.State => _state;

        public AnimatorState State { get; private set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() =>
            _animator.SetTrigger(_hit);

        public void PlayDeath() =>
            _animator.SetTrigger(_die);

        public void Move(float speed)
        {
            _animator.SetBool(_isMoving, true);
            _animator.SetFloat(_speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(_isMoving, false);

        public void PlayAttack() =>
            _animator.SetTrigger(_attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}
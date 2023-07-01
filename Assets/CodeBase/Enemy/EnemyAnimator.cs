using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int s_attack = Animator.StringToHash("Attack_1");
        private static readonly int s_speed = Animator.StringToHash("Speed");
        private static readonly int s_isMoving = Animator.StringToHash("IsMoving");
        private static readonly int s_hit = Animator.StringToHash("Hit");
        private static readonly int s_die = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator _animator;
        private Logic.AnimatorState _state;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(State);

        Logic.AnimatorState IAnimationStateReader.State => _state;

        public AnimatorState State { get; private set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() =>
            _animator.SetTrigger(s_hit);

        public void PlayDeath() =>
            _animator.SetTrigger(s_die);

        public void Move(float speed)
        {
            _animator.SetBool(s_isMoving, true);
            _animator.SetFloat(s_speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(s_isMoving, false);

        public void PlayAttack() =>
            _animator.SetTrigger(s_attack);

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
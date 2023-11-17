using System;
using UnityEngine;

public class HeroAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int OnDie = Animator.StringToHash("OnDie");
    private static readonly int IsHeadTilted = Animator.StringToHash("IsHeadTilted");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int TiltHeadSpeed = Animator.StringToHash("TiltHeadSpeed");
    private static readonly int OnPutBlock = Animator.StringToHash("OnPutBlock");

    private static readonly int _idle = Animator.StringToHash("Idle");
    private static readonly int _attack = Animator.StringToHash("Attack");
    private static readonly int _tiltHead = Animator.StringToHash("TiltHead");
    private static readonly int _moving = Animator.StringToHash("Moving");
    private static readonly int _putBlock = Animator.StringToHash("PutBlock");
    private static readonly int _died = Animator.StringToHash("Died");

    private Animator _animator;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    private void Awake() => 
        _animator = GetComponent<Animator>();

    public void StartMove() =>
        _animator.SetBool(IsMoving, true);

    public void StopMove() =>
        _animator.SetBool(IsMoving, false);

    public void StartDie() =>
        _animator.SetTrigger(OnDie);

    public void StartHeadTilted(float speed)
    {
        _animator.SetBool(IsHeadTilted, true);
        _animator.SetFloat(TiltHeadSpeed, speed);
    }

    public void StopIsHeadTilted() => 
        _animator.SetBool(IsHeadTilted, false);

    public void OnPutBlockTrigger()
    {
        _animator.SetTrigger(OnPutBlock);
    }

    public void StartAttack() =>
        _animator.SetBool(IsAttack, true);

    public void StopAttack() => 
        _animator.SetBool(IsAttack, false);

    public void EnteredState(int stateHash) => 
        StateEntered?.Invoke(StateFor(stateHash));

    public void ExitedState(int stateHash) => 
        StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;

        if (stateHash == _idle)
            state = AnimatorState.Idle;
        else if (stateHash == _attack)
            state = AnimatorState.Attack;
        else if (stateHash == _tiltHead)
            state = AnimatorState.HeadTitled;
        else if (stateHash == _moving)
            state = AnimatorState.Moving;
        else if (stateHash == _putBlock)
            state = AnimatorState.PutBlock;
        else if (stateHash == _died)
            state = AnimatorState.Died;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}

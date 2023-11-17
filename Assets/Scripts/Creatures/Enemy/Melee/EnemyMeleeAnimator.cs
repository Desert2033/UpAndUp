using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAnimator : MonoBehaviour
{
    private static readonly int OnAttack01 = Animator.StringToHash("OnAttack01");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsIdleNormal = Animator.StringToHash("IsIdleNormal");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnAttack()
    {
        _animator.SetTrigger(OnAttack01);
    }

    public void Die()
    {
        _animator.SetBool(IsDie, true);
    }

    public void StartIdle()
    {
        _animator.SetBool(IsIdleNormal, true);
    }

    public void StopIdle()
    {
        _animator.SetBool(IsIdleNormal, false);
    }

    public void StartJump()
    {
        _animator.SetBool(IsJump, true);
    }

    public void StopJump()
    {
        _animator.SetBool(IsJump, false);
    }
}

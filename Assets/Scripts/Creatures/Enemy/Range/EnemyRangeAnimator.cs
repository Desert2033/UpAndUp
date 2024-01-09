using System;
using UnityEngine;

public class EnemyRangeAnimator : MonoBehaviour
{
    private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnAttack()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    public void StartDie()
    {
        _animator.SetBool(IsDie, true);
    }
}

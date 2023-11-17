using UnityEngine;

public class EnemyRangeAnimator : MonoBehaviour
{
    private static readonly int OnAttack01 = Animator.StringToHash("OnAttack01");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
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

    public void StartDie()
    {
        _animator.SetBool(IsDie, true);
    }

    public void StopDie()
    {
        _animator.SetBool(IsDie, false);
    }

    public void StartIdle()
    {
        _animator.SetBool(IsIdleNormal, true);
    }

    public void StopIdle()
    {
        _animator.SetBool(IsIdleNormal, false);
    }
}

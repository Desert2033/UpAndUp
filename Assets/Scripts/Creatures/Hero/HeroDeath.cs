using System;
using System.Collections;
using UnityEngine;

public class HeroDeath : MonoBehaviour
{
    private const float WaitUntilDestroySeconds = 1f;

    [SerializeField] private HeroHealth _heroHealth;
    [SerializeField] private TakeDamageAnimation _heroDamageAnimation;
    [SerializeField] private HeroAnimator _heroAnimator;
    [SerializeField] private BoxCollider _boxCollider;

    public event Action OnDie;

    private void OnEnable()
    {
        _heroHealth.OnHpRunOut += Dead;
    }

    private void OnDisable()
    {
        _heroHealth.OnHpRunOut -= Dead;
    }

    private void Dead()
    {
        _boxCollider.isTrigger = true;
        _heroDamageAnimation.PlayDeath();
        _heroAnimator.StartDie();

        OnDie?.Invoke();

        StartCoroutine(WaitUntilDestroy());
    }

    private IEnumerator WaitUntilDestroy()
    {
        yield return new WaitForSeconds(WaitUntilDestroySeconds);

        Destroy(gameObject);
    }
}

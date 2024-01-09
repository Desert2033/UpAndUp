using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private const float DurationUntilDestroy = 1f;

    [SerializeField] private EnemyRangeAnimator _enemyAnimator;
    [SerializeField] private EnemyRangeAttack _enemyAttack;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private TakeDamageAnimation _damageAnimation;
    [SerializeField] private GameObject _hpBar;

    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    private void OnEnable()
    {
        _enemyHealth.OnHpRunOut += Dead;
    }

    private void OnDisable()
    {
        _enemyHealth.OnHpRunOut -= Dead;

        IReactionOfHeroDeath[] components = gameObject.GetComponents<IReactionOfHeroDeath>();

        foreach (IReactionOfHeroDeath component in components)
        {
            _gameFactory.RemoveFromReactionOfHeroDeath(component);
        }
    }

    public void Dead()
    {
        _enemyAttack.enabled = false;
        _hpBar.SetActive(false);

        StartCoroutine(PlayAnimationAndDestroy());
    }

    private IEnumerator PlayAnimationAndDestroy()
    {
        _damageAnimation.PlayDeath();
       _enemyAnimator.StartDie();
        yield return new WaitForSeconds(DurationUntilDestroy);

        _enemyAnimator.StartDie();
        Destroy(gameObject);
    }
}

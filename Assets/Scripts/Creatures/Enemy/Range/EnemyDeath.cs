using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private const float DurationUntilDestroy = 1f;

    [SerializeField] private EnemyRangeAnimator _enemyAnimator;
    [SerializeField] private EnemyRangeAttack _enemyAttack;
    [SerializeField] private EnemyRangeObserver _enemyObserver;
    [SerializeField] private EnemyHealth _enemyHealth;

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
        _enemyObserver.enabled = false;

        StartCoroutine(PlayAnimationAndDestroy());
    }

    private IEnumerator PlayAnimationAndDestroy()
    {
        _enemyAnimator.StartDie();
        yield return new WaitForSeconds(DurationUntilDestroy);

        _enemyAnimator.StartDie();
        Destroy(gameObject);
    }
}

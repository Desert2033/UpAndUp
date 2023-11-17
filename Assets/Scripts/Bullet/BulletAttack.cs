using UnityEngine;

public class BulletAttack : MonoBehaviour, IReactionOfHeroDeath
{
    private float _damage;
    private Transform _targetTransform;
    private IGameFactory _gameFactory;

    public void Construct(float damage, Transform targetTransform, IGameFactory gameFactory)
    {
        _damage = damage;
        _targetTransform = targetTransform;
        _gameFactory = gameFactory;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _targetTransform)
        {
            other.GetComponent<IHealth>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _gameFactory.RemoveFromReactionOfHeroDeath(this);
    }

    public void OnHeroDie()
    {
        Destroy(gameObject);
    }
}

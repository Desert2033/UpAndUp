using UnityEngine;

public class BulletAttack : MonoBehaviour, IReactionOfHeroDeath
{
    private const float LifeTime = 1.5f;

    private float _damage;
    private Transform _targetTransform;
    private IGameFactory _gameFactory;
    private Timer _lifeTime;

    public void Construct(float damage, Transform targetTransform, IGameFactory gameFactory)
    {
        _damage = damage;
        _targetTransform = targetTransform;
        _gameFactory = gameFactory;
    }

    private void Start()
    {
        _lifeTime = new Timer(LifeTime);
    }

    private void Update()
    {
        _lifeTime.Tick(Time.deltaTime);

        if (_lifeTime.CurrentDuretion <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == _targetTransform)
        {
            collision.transform.GetComponent<IHealth>().TakeDamage(_damage);
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

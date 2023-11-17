using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour, IReactionOfHeroDeath
{
    private EnemyRangeAnimator _enemyAnimator;
    private HeroHealth _heroHealth;
    private CameraBorder _cameraBorder;
    private IGameFactory _gameFactory;
    private Timer _cooldown;
    private float _damage = 1; 
    private float _duration = 1.5f;
    private float _offsetY;

    public void Construct(HeroHealth heroHealth, CameraBorder cameraBorder, IGameFactory gameFactory, float damage)
    {
        _heroHealth = heroHealth;
        _cameraBorder = cameraBorder;
        _gameFactory = gameFactory;
        _damage = damage;
        _cooldown = new Timer(_duration);
        _offsetY = Constants.EnemyRangeOffsetToCenterY;
    }

    private void Start()
    {
        _enemyAnimator = GetComponent<EnemyRangeAnimator>();
    }

    private void Update()
    {
        _cooldown.Tick(Time.deltaTime);

        if (CanAttack())
        {
            Attack();
            _cooldown.Restart();
        }
    }

    public void Attack()
    {
        SpawnBullet();
        _enemyAnimator.OnAttack();
    }

    public void OnHeroDie()
    {
        enabled = false;
    }

    private bool CanAttack() =>
        transform.position.y + _offsetY <= _cameraBorder.RightTop.y && _cooldown.CurrentDuretion <= 0f;

    private void SpawnBullet() => 
        _gameFactory.CreateBullet(transform.position, 
            _heroHealth.transform.position - transform.position, 
            _damage, 
            AssetPath.PathEnemyBullet,
            _heroHealth.transform);
}

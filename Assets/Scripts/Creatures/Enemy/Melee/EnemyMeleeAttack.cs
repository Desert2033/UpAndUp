using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private EnemyMeleeAnimator _enemyAnimator;
    private HeroHealth _heroHealth;
    private CameraBorder _cameraBorder;
    private Timer _cooldown;
    private int _damage = 1;
    private float _duration = 1f;
    private float _offsetY = 1.2f;

    public void Construct(HeroHealth heroHealth, CameraBorder cameraBorder)
    {
        _heroHealth = heroHealth;
        _cameraBorder = cameraBorder;
        _cooldown = new Timer(_duration);
    }

    private void Start()
    {
        _enemyAnimator = GetComponent<EnemyMeleeAnimator>();
    }

    private void Update()
    {
        if (transform.position.y + _offsetY <= _cameraBorder.RightTop.y)
        {
            _cooldown.Tick(Time.deltaTime);

            if (_cooldown.CurrentDuretion <= 0)
            {
                _heroHealth.TakeDamage(_damage);

                _cooldown.Restart();
            }
        }
    }

    public void Attack()
    {
        _enemyAnimator.OnAttack();
    }
}

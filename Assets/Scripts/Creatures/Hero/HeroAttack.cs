using System.Collections;
using UnityEngine;

public class HeroAttack : MonoBehaviour, IReactionOfHeroDeath
{
    private const float DurationUntilDisactiveAttack = 0.4f;

    [SerializeField] private Animator _bowAnimator;
    [SerializeField] private HeroAnimator _heroAnimator;
    [SerializeField] private GameObject _blockForPut;
    [SerializeField] private HeroMovingBehaviour _heroMoving;
    [SerializeField] private HeroLevelChanger _levelChanger;

    private float _damage;
    private IGameFactory _gameFactory;
    private IInputService _inputService;
    private bool _isAttack = false;

    public void Construct(IGameFactory gameFactory, IInputService inputService)
    {
        _gameFactory = gameFactory;
        _inputService = inputService;
    }

    private void Update()
    {
        Actions action = _inputService.GetActions(out Transform enemyTransform); 
        
        if (action == Actions.Attack && !_isAttack)
        {
           StartCoroutine(Attack(enemyTransform));
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void OnHeroDie()
    {
        this.enabled = false;
    }

    public IEnumerator Attack(Transform enemyTransform)
    {
        ActiveAttack();

        yield return new WaitForSeconds(DurationUntilDisactiveAttack);

        CreateBullet(enemyTransform);

        DisactiveAttack();
    }

    private void DisactiveAttack()
    {
        _bowAnimator.gameObject.SetActive(false);
        _blockForPut.SetActive(true);
        _heroMoving.enabled = true;
        _levelChanger.enabled = true;

        _heroAnimator.StopAttack();
        _isAttack = false;
    }

    private void ActiveAttack()
    {
        _bowAnimator.gameObject.SetActive(true);
        _heroAnimator.StartAttack();

        _blockForPut.SetActive(false);
        _heroMoving.enabled = false;
        _levelChanger.enabled = false;

        _bowAnimator.SetTrigger("BowAttack");
        _isAttack = true;
    }

    private void CreateBullet(Transform enemyTransform)
    {
        if (enemyTransform != null)
        {
            _gameFactory.CreateBullet(_bowAnimator.transform.position,
                enemyTransform.position - transform.position,
                _damage,
                AssetPath.PathHeroBullet,
                enemyTransform);
        }
    }
}
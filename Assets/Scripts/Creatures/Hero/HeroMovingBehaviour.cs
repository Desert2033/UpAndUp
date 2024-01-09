using System;
using UnityEngine;

public class HeroMovingBehaviour : MonoBehaviour, ICoroutineRunner, IReactionOfHeroDeath
{
    private const float DuretionCooldown = 0.6f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private HeroAnimator _heroAnimator;
    [SerializeField] private HeroMoveSides _heroMoveSide;
    [SerializeField] private HeroJump _heroJump;

    private IInputService _inputService;
    private Directions _prevDirection;
    private HeroPutBlock _heroPutBlock;
    private Timer _cooldownMove;

    public Action OnMoveUp;

    private void Awake()
    {
        IGameFactory gameFactory = AllServices.Container.Single<IGameFactory>();

        _inputService = AllServices.Container.Single<IInputService>();
        _heroPutBlock = new HeroPutBlock(gameFactory, _heroAnimator);
        _cooldownMove = new Timer(DuretionCooldown);
    }

    private void Update()
    {
        Actions action = _inputService.GetActions(out Transform enemyTransform);
       
        if (action == Actions.Move) 
        {
            Directions direction = _inputService.GetDirection();

            _cooldownMove.Tick(Time.deltaTime);

            if (CanMove(direction))
            {
                Move(direction);

                _cooldownMove.Restart();
            }
        }
    }

    public void OnHeroDie()
    {
        this.enabled = false;
    }

    private void Move(Directions direction)
    {
        _prevDirection = direction;

        Vector3 blockPosition = _heroPutBlock.PutBlock(direction);

        switch (direction)
        {
            case Directions.Up:
                _heroJump.Jump();
                OnMoveUp?.Invoke();
                break;
            default:
               _heroMoveSide.Move(direction, blockPosition);
                break;
        }
    }

    private bool CanMove(Directions direction) =>
        (_cooldownMove.CurrentDuretion <= 0f
        || _prevDirection != direction)
        && !_heroMoveSide.IsMoving
        && !_heroJump.IsMovingUp;
}

using UnityEngine;

public class EnemyRangeObserver : MonoBehaviour, IReactionOfHeroDeath
{
    private const float DeltaY = 7f;
    private const float DeltaX = 10f;
    private const float ConstRotationY = 180f;

    private Transform _heroTransform;
    private CameraBorder _cameraBorder;
    private IGameFactory _gameFactory;
    private float _speed = 2f;
    private float _offsetY;

    public void Construct(Transform heroTransform, CameraBorder cameraBorder, IGameFactory gameFactory)
    {
        _heroTransform = heroTransform;
        _cameraBorder = cameraBorder;
        _gameFactory = gameFactory;
        _offsetY = Constants.EnemyRangeOffsetToCenterY;
    }

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        if (_cameraBorder == null)
            return;

        if (CanRotation())
        {
            Vector3 direction = transform.position - _heroTransform.transform.position;
            Quaternion rotation = Quaternion.Euler(RotationX(direction), RotationY(direction), 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speed);
        }
    }

    private bool CanRotation() =>
        transform.position.y + _offsetY <= _cameraBorder.RightTop.y;

    private float RotationX(Vector3 direction) =>
        direction.y * DeltaY;

    private float RotationY(Vector3 direction) =>
        direction.x * DeltaX + ConstRotationY;

    private void OnDestroy()
    {
        _gameFactory.RemoveFromReactionOfHeroDeath(this);
    }

    public void OnHeroDie()
    {
        enabled = false;
    }
}

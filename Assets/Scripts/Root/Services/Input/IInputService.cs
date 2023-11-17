using UnityEngine;

public interface IInputService : IService
{
    public void SetInputUI(InputUI inputUI);

    public Directions GetDirection();
    public Actions GetActions(out Transform enemyTransform);
}

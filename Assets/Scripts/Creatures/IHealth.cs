using System;

public interface IHealth
{
    public void TakeDamage(float damage);

    public event Action<float, float> OnHpChange;
}

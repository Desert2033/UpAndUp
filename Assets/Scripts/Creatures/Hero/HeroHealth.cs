using System;
using UnityEngine;

public class HeroHealth : MonoBehaviour, IHealth
{
    [SerializeField] private HeroTakeDamageAnimation _damageAnimation;

    private float _maxHP = 5;
    private float _currentHP = 0;

    public float CurrentHP
    {
        get => _currentHP;
     
        private set 
        {
            OnHpChange?.Invoke(value, _maxHP);
            _currentHP = value;
        }
    }

    public event Action OnHpRunOut;
    public event Action<float, float> OnHpChange;

    public void SetMaxHp(float maxHp)
    {
        _maxHP = maxHp;

        CurrentHP = _maxHP;
    }

    public void TakeDamage(float damage)
    {
        _damageAnimation.PlayTakeDamage();
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            OnHpRunOut?.Invoke();
            return;
        }
    }
}

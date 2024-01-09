using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private TakeDamageAnimation _damageAnimation;

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

    public void Construct(float maxHP)
    {
        _maxHP = maxHP;
    }

    private void Start()
    {
        CurrentHP = _maxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;

        _damageAnimation.PlayTakeDamage();

        if (CurrentHP <= 0)
        {
            OnHpRunOut?.Invoke();
            return;
        }
    }
}

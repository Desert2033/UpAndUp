using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private float _duration = 0.2f;
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
        StartCoroutine(TakeDamageAnimation());
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            OnHpRunOut?.Invoke();
            return;
        }
    }

    public IEnumerator TakeDamageAnimation()
    {
        _meshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(_duration);

        _meshRenderer.material.color = Color.white;
    }
}

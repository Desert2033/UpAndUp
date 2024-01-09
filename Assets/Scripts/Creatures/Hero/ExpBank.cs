using System;
using UnityEngine;

public class ExpBank : MonoBehaviour
{
    private float _maxExp = 1000;
    private float _currentExp = 0;

    public Action<float> OnExpChange;
    public Action OnExpMax;

    public void AddExp(float exp)
    {
        _currentExp += exp;
        
        if (_currentExp >= _maxExp)
        {
            _currentExp -= _maxExp;
            OnExpMax?.Invoke();
        }

        OnExpChange?.Invoke(_currentExp);
    }

}

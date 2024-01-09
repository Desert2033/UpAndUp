using System;
using UnityEngine;

public class HealBank : MonoBehaviour
{
    private int _countHeal = 0;

    public event Action<int> OnChangeCount;

    public void Add()
    {
        _countHeal++;

        OnChangeCount?.Invoke(_countHeal);
    }

    public void Remove()
    {
        _countHeal--;

        if (_countHeal <= 0)
            _countHeal = 0;


        OnChangeCount?.Invoke(_countHeal);
    }

    public bool HaveHeal() => 
        _countHeal > 0;
}

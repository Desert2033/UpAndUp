using System;
using TMPro;
using UnityEngine;

public class CounterBlocks : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private HeroMovingBehaviour _heroMoving;
    private uint _countBlocks;

    public uint CountBlocks => _countBlocks;

    public Action<uint> OnChangeCount;

    public void Construct(HeroMovingBehaviour heroMoving)
    {
        _heroMoving = heroMoving;

        _heroMoving.OnMoveUp += AddBlockInCount;
    }

    private void OnDisable()
    {
        if (_heroMoving != null)
            _heroMoving.OnMoveUp -= AddBlockInCount;
    }

    private void AddBlockInCount()
    {
        _countBlocks++;

        _counterText.text = _countBlocks.ToString();

        OnChangeCount?.Invoke(_countBlocks);
    }
}

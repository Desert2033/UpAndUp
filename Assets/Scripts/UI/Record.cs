using TMPro;
using UnityEngine;

public class Record : MonoBehaviour, ISavedProgress
{
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private CounterBlocks _counterBlocks;

    private uint _record;

    private void OnEnable()
    {
        _recordText.text = _record.ToString();

        _counterBlocks.OnChangeCount += ChangeRecord;
    }

    private void OnDisable() => 
        _counterBlocks.OnChangeCount -= ChangeRecord;

    public void LoadProgess(PlayerProgress progress)
    {
        _record = progress.GameData.Record;

        _recordText.text = _record.ToString();
    }

    public void UpdateProgress(PlayerProgress progress) => 
        progress.GameData = new GameData(_record);

    private void ChangeRecord(uint countBlocks)
    {
        if (_record < countBlocks)
        {
            _record = countBlocks;

            _recordText.text = _record.ToString();
        }
    }
}

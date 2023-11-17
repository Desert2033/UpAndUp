using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRangeData", menuName = "ScriptableObjects/Enemy Range Levels")]
public class EnemyRangeData : ScriptableObject
{
    [Min(1)]
    public int Level;
    public int MinBlocks;
    public int MaxBlocks;
    public float Health;
    public float Damage;
}

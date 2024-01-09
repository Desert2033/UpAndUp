using UnityEngine;
using System;

[Serializable]
public struct SpawnObstacle
{
    public TypeObstacles Type;
    public float SpawnProcent;
    public float DamageMin;
    public float DamageMax;
    public float HPMin;
    public float HPMax;
}

[Serializable]
public struct SpawnBonus
{
    public TypeBonus Type;
    public float SpawnProcent;
}

public enum TypeObstacles
{
    TNT,
    TNTMoveEasy,
    TNTMoveHard,
    TNTMoveDown,
    TNTWall,
    Skeleton
}

public enum TypeBonus
{
    Experience,
    Health
}

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/Spawner Data")]
public class SpawnerData : ScriptableObject
{   
    public float Duration;
    public int MaxBlocks;
    public int MinBlocks;
    public int ExperienceProcent;
    public SpawnBonus[] SpawnBonuses; 
    public SpawnObstacle[] SpawnObstacles;
}

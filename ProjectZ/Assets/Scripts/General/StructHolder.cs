using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemies
[System.Serializable]
public struct EnemyStats
{
    public int level;
    public int HP;
    public int SP;
    public int attackPower;
    public int defence;
    public int agility;
}

[System.Serializable]
public struct Enemies
{
    public GameObject E_Cube;
    public GameObject E_Capsule;
    public GameObject E_Sphere;
}

[System.Serializable]
public struct EnemyStatMultipliers
{
    public float stageStatMultiplier;
    public float normalStatMultiplier;
    public float normalRareStatMultiplier;
    public float miniBossStatMultiplier;
    public float miniBossRareStatMultiplier;
    public float bossStatMultiplier;
    public float bossRareStatMultiplier;
}

[System.Serializable]
public struct EnemyChances
{
    public float miniBossChance;
    public float normalRareChance;
    public float miniBossRareChance;
    public float bossRareChance;
}

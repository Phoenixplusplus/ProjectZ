using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemies
[System.Serializable]
public struct EnemyStats
{
    public int level;
    public int HP;
    public int maxHP;
    public int SP;
    public int maxSP;
    public int attackPower;
    public int defence;
    public int agility;
    public float evadeChance;
    public float criticalChance;
    public int criticalDamage;
    public float criticalMultiplier;
    public int exp;
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

// Player
[System.Serializable]
public struct PlayerStats
{
    public string name;
    public int level;
    public int HP;
    public int maxHP;
    public int SP;
    public int maxSP;
    public int attackPower;
    public int defence;
    public int agility;
    public int accuracy;
    public int luck;
    public float evadeChance;
    public float criticalChance;
    public int criticalDamage;
    public float criticalMultiplier;
    public int exp;
    public int expToNextLevel;
}

[System.Serializable]
public struct PlayerStatMultipliers
{
    public float expToNextLevelMultiplier;
}

// Level
[System.Serializable]
public struct FloorPrefabs
{
    public GameObject grassFloor;
    public GameObject sandFloor;
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemies enemies;

    [Header("Stat Multipliers")]
    public EnemyStatMultipliers enemyStatMultipliers;

    [Header("Enemy Chances")]
    public EnemyChances enemyChances;

    GameObject madeEnemy;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    public GameObject MakeEnemy(EnemyName enemyName, EnemyImportance enemyImportance, bool randomRareImportance, int stage, Vector3 position, Vector3 rotation)
    {
        switch (enemyName)
        {
            case EnemyName.E_Cube:
                madeEnemy = Instantiate(enemies.E_Cube, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, enemyImportance, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            case EnemyName.E_Capsule:
                madeEnemy = Instantiate(enemies.E_Capsule, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, enemyImportance, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            case EnemyName.E_Sphere:
                madeEnemy = Instantiate(enemies.E_Sphere, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, enemyImportance, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            default: return madeEnemy;
        }
    }

    public GameObject MakeEnemy(EnemyName enemyName, bool randomRareImportance, int stage, Vector3 position, Vector3 rotation)
    {
        switch (enemyName)
        {
            case EnemyName.E_Cube:
                madeEnemy = Instantiate(enemies.E_Cube, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            case EnemyName.E_Capsule:
                madeEnemy = Instantiate(enemies.E_Capsule, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            case EnemyName.E_Sphere:
                madeEnemy = Instantiate(enemies.E_Sphere, position, Quaternion.identity);
                madeEnemy.transform.eulerAngles = rotation;
                ConfigureEnemy(madeEnemy, randomRareImportance, stage);
                madeEnemy.transform.parent = this.gameObject.transform;
                return madeEnemy;
            default: return madeEnemy;
        }
    }

    // Main function for configuring enemy data, will call all other functions
    // overload with EnemyImportance if a set importance is desired, else it will randomise it between Normal and MiniBoss
    void ConfigureEnemy(GameObject madeEnemy, EnemyImportance enemyImportance, bool randomRareImportance, int stage)
    {
        Enemy enemyComponent = madeEnemy.GetComponentInChildren<Enemy>();
        enemyComponent.enemyImportance = enemyImportance;
        if (randomRareImportance) enemyComponent.enemyImportance = RandomiseRareImportance(enemyImportance);
        enemyComponent.enemyStats = ConfigureStats(enemyComponent.enemyStats, enemyComponent.enemyImportance, stage);
    }

    void ConfigureEnemy(GameObject madeEnemy, bool randomRareImportance, int stage)
    {
        Enemy enemyComponent = madeEnemy.GetComponentInChildren<Enemy>();
        enemyComponent.enemyImportance = RandomiseImportance();
        if (randomRareImportance) enemyComponent.enemyImportance = RandomiseRareImportance(enemyComponent.enemyImportance);
        enemyComponent.enemyStats = ConfigureStats(enemyComponent.enemyStats, enemyComponent.enemyImportance, stage);
    }

    // Stat multipliers
    float GetStageMultiplier(int stage) { return stage * enemyStatMultipliers.stageStatMultiplier; }
    float GetImportanceMultiplier(EnemyImportance enemyImportance)
    {
        switch (enemyImportance)
        {
            case EnemyImportance.Normal: return 1f;
            case EnemyImportance.Rare: return 1.1f;
            case EnemyImportance.MiniBoss: return 1.2f;
            case EnemyImportance.RareMiniBoss: return 1.3f;
            case EnemyImportance.Boss: return 1.4f;
            case EnemyImportance.RareBoss: return 1.8f;
            default: return 1f;
        }
    }

    // Give random Importance if not created with one. Boss/RareBoss not included, they must be explicitly created
    EnemyImportance RandomiseImportance()
    {
        if (Random.Range(1f, 100f) <= enemyChances.miniBossChance) return EnemyImportance.MiniBoss;
        else return EnemyImportance.Normal;
    }

    // Give rare chance
    EnemyImportance RandomiseRareImportance(EnemyImportance enemyImportance)
    {
        switch (enemyImportance)
        {
            case EnemyImportance.Normal:
                if (Random.Range(1f, 100f) <= enemyChances.normalRareChance) return EnemyImportance.Rare;
                else return EnemyImportance.Normal;
            case EnemyImportance.MiniBoss:
                if (Random.Range(1f, 100f) <= enemyChances.miniBossRareChance) return EnemyImportance.RareMiniBoss;
                else return EnemyImportance.MiniBoss;
            case EnemyImportance.Boss:
                if (Random.Range(1f, 100f) <= enemyChances.bossRareChance) return EnemyImportance.RareBoss;
                else return EnemyImportance.Boss;
            default: return enemyImportance;
        }
    }

    // Configure Stats
    EnemyStats ConfigureStats(EnemyStats enemyStats, EnemyImportance enemyImportance, int stage)
    {
        return new EnemyStats()
        {
            level = stage,
            HP = Mathf.RoundToInt((enemyStats.HP * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            maxHP = Mathf.RoundToInt((enemyStats.HP * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            SP = Mathf.RoundToInt((enemyStats.SP * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            maxSP = Mathf.RoundToInt((enemyStats.SP * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            attackPower = Mathf.RoundToInt((enemyStats.attackPower * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            defence = Mathf.RoundToInt((enemyStats.defence * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            agility = Mathf.RoundToInt((enemyStats.agility * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)),
            evadeChance = enemyStats.evadeChance * GetImportanceMultiplier(enemyImportance),                                                    // Evade chance is not affected by stage
            criticalMultiplier = enemyStats.criticalMultiplier * GetImportanceMultiplier(enemyImportance),                                      // Critical multiplier is not affected by stage
            criticalChance = enemyStats.criticalChance * GetImportanceMultiplier(enemyImportance),                                              // Critical chance is not affected by stage
            criticalDamage = Mathf.RoundToInt((Mathf.RoundToInt((enemyStats.attackPower * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance)) * (enemyStats.criticalMultiplier * GetImportanceMultiplier(enemyImportance))) * GetImportanceMultiplier(enemyImportance)),
            exp = Mathf.RoundToInt((enemyStats.exp * GetStageMultiplier(stage)) * GetImportanceMultiplier(enemyImportance))
        };
    }

    // Get random enemy name
    public EnemyName GetRandomEnemyName()
    {
        return (EnemyName)Random.Range(0, System.Enum.GetValues(typeof(EnemyName)).Length);
    }
}

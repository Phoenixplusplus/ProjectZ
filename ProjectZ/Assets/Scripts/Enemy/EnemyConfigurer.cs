using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfigurer : MonoBehaviour
{
    Enemy enemy = null;

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

    public void ConfigureTakeDamage(Enemy enemy, PlayerStats playerStats)
    {
        EnemyStats enemyStats = enemy.enemyStats;

        int damageTaken = playerStats.attackPower;
        int criticalDamage = playerStats.criticalDamage;
        float criticalChance = playerStats.criticalChance;

        // Calculate if damage will be critical
        bool isCritical;
        if (Random.Range(0f, 100f) <= criticalChance) isCritical = true;
        else isCritical = false;

        // Check if attack will be evaded. If damage is critical, it cannot be evaded
        if (Random.Range(0f, 100f) <= enemyStats.evadeChance) damageTaken = 0;

        if (isCritical) enemy.TakeDamage(criticalDamage, isCritical);
        else enemy.TakeDamage(damageTaken, isCritical);
    }

    public EnemyStats GetEnemyStats() { return enemy.enemyStats; }
}

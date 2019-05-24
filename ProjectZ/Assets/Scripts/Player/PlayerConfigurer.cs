using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigurer : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField]
    Player player = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        ConfigurePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    public void ConfigureEXP(int exp, float expToNextLevelMultiplier)
    {
        player.playerStats.exp += exp;
        if (player.playerStats.exp == player.playerStats.expToNextLevel)
        {
            player.playerStats.exp = 0;
            player.playerStats.level++;
            player.playerStats.expToNextLevel = Mathf.RoundToInt((player.playerStats.expToNextLevel * 2) * expToNextLevelMultiplier);
            Debug.Log("Level up! Player now level: " + player.playerStats.level);
        }
        if (player.playerStats.exp > player.playerStats.expToNextLevel)
        {
            player.playerStats.exp = player.playerStats.exp - player.playerStats.expToNextLevel;
            player.playerStats.level++;
            player.playerStats.expToNextLevel = Mathf.RoundToInt((player.playerStats.expToNextLevel * 2) * expToNextLevelMultiplier);
            Debug.Log("Level up! Player now level: " + player.playerStats.level);
        }
        Debug.Log("Player EXP now: " + player.playerStats.exp + " with " + player.playerStats.expToNextLevel + " to next level");
    }

    public void ConfigurePlayerStats()
    {
        player.playerStats.criticalDamage = Mathf.RoundToInt(player.playerStats.attackPower * player.playerStats.criticalMultiplier);
    }

    public void ConfigureTakeDamage(Enemy attackingEnemy)
    {
        EnemyStats enemyStats = attackingEnemy.enemyStats;
        int damageTaken = enemyStats.attackPower;
        int criticalDamage = enemyStats.criticalDamage;
        float criticalChance = enemyStats.criticalChance;

        // Calculate if damage will be critical
        bool isCritical;
        if (Random.Range(0f, 100f) <= criticalChance) isCritical = true;
        else isCritical = false;

        // Check if attack will be evaded. If damage is critical, it cannot be evaded
        if (Random.Range(0f, 100f) <= player.playerStats.evadeChance) damageTaken = 0;

        if (isCritical) player.TakeDamage(criticalDamage, isCritical);
        else player.TakeDamage(damageTaken, isCritical);
    }

    public PlayerStats GetPlayerStats() { return player.playerStats; }
    public float GetPlayerCriticalChance() { return player.playerStats.criticalChance; }
}

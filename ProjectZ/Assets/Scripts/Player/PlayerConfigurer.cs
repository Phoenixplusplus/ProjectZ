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

    public PlayerStats GetPlayerStats() { return player.playerStats; }
    public bool GetPlayerCriticalHit()
    {
        if (Random.Range(0f, 100f) <= player.playerStats.criticalChance) return true;
        else return false;
    }
}

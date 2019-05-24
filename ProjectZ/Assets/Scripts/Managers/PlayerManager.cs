using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Multipliers")]
    [SerializeField]
    PlayerStatMultipliers playerStatMultipliers;

    PlayerConfigurer playerConfigurer = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        playerConfigurer = GetComponent<PlayerConfigurer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    
    public PlayerStats GetPlayerStats() { return playerConfigurer.GetPlayerStats(); }
    public float GetPlayerCriticalChance() { return playerConfigurer.GetPlayerCriticalChance(); }
    public void GivePlayerEXP(int exp) { playerConfigurer.ConfigureEXP(exp, playerStatMultipliers.expToNextLevelMultiplier); }
    public void PlayerTakeDamage(Enemy attackingEnemy) { playerConfigurer.ConfigureTakeDamage(attackingEnemy); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Multipliers")]
    [SerializeField]
    PlayerStatMultipliers playerStatMultipliers;

    [Header("Class References")]
    [SerializeField]
    PlayerConfigurer playerConfigurer = null;

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

    public void GivePlayerEXP(int exp)
    {
        playerConfigurer.ConfigureEXP(exp, playerStatMultipliers.expToNextLevelMultiplier);
    }
    public PlayerStats GetPlayerStats() { return playerConfigurer.GetPlayerStats(); }
    public bool GetPlayerCriticalHit() { return playerConfigurer.GetPlayerCriticalHit(); }
}

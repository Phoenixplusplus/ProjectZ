using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("InGame UI Class References")]
    [SerializeField]
    PlayerNameHPSPEXP_UI playerNameHPSPEXP_UI = null;

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

    // InGame UI
    // PlayerNameHPSPEXP_UI
    public void ConfigurePlayerNameHPSPEXP_UI(PlayerStats playerStats) { playerNameHPSPEXP_UI.Configure(playerStats.HP, playerStats.maxHP, playerStats.SP, playerStats.maxSP, playerStats.exp, playerStats.expToNextLevel, playerStats.level, playerStats.name); }
    public void UpdatePlayerHP(PlayerStats playerStats) { playerNameHPSPEXP_UI.UpdatePlayerHP(playerStats.HP, playerStats.maxHP); }
    public void UpdatePlayerSP(PlayerStats playerStats) { playerNameHPSPEXP_UI.UpdatePlayerSP(playerStats.SP, playerStats.maxSP); }
    public void UpdatePlayerEXP(PlayerStats playerStats) { playerNameHPSPEXP_UI.UpdatePlayerEXP(playerStats.exp, playerStats.expToNextLevel, playerStats.level, playerStats.name); }
}

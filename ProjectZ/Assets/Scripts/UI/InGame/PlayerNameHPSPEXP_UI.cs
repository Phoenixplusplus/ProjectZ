using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameHPSPEXP_UI : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField]
    Text playerName = null;
    [SerializeField]
    Slider hpSlider = null;
    [SerializeField]
    Slider spSlider = null;
    [SerializeField]
    Slider expSlider = null;
    [SerializeField]
    Text hpText = null;
    [SerializeField]
    Text spText = null;
    [SerializeField]
    Text expText = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }
    #endregion

    public void Configure(int hp, int maxHP, int sp, int maxSP, int exp, int expToNextLevel, int level, string name)
    {
        this.gameObject.SetActive(true);
        UpdatePlayerHP(hp, maxHP);
        UpdatePlayerSP(sp, maxSP);
        UpdatePlayerEXP(exp, expToNextLevel, level, name);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdatePlayerHP(int hp, int maxHP)
    {
        hpText.text = "HP: " + hp + "/" + maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = hp;
    }

    public void UpdatePlayerSP(int sp, int maxSP)
    {
        spText.text = "SP: " + sp + "/" + maxSP;
        spSlider.maxValue = maxSP;
        spSlider.value = sp;
    }

    public void UpdatePlayerEXP(int exp, int expToNextLevel, int level, string name)
    {
        expText.text = "EXP: " + exp;
        expSlider.maxValue = expToNextLevel;
        expSlider.value = exp;
        UpdatePlayerName(level, name);
    }

    public void UpdatePlayerName(int level, string name)
    {
        playerName.text = "Lv." + level + " " + name;
    }
}

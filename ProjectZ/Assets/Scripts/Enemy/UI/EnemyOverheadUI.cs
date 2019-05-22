using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOverheadUI : MonoBehaviour
{
    [Header("Overhead UI")]
    [SerializeField]
    Color rareColour;
    [SerializeField]
    Color miniBossColour;
    [SerializeField]
    Color rareMiniBossColour;
    [SerializeField]
    Color bossColour;
    [SerializeField]
    Color RareBossColour;

    [Header("References")]
    [SerializeField]
    Text enemyName_UI;
    [SerializeField]
    Text enemyImportance_UI;
    [SerializeField]
    Slider enemyHealth_UI;

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

    public void ConfigureOverheadUI(EnemyStats enemyStats, EnemyName enemyName, EnemyImportance enemyImportance)
    {
        ConfigureImportanceUI(enemyImportance);
        enemyName_UI.text = "Lv." + enemyStats.level + " " + enemyName.ToString();
        enemyHealth_UI.maxValue = enemyStats.HP;
        enemyHealth_UI.value = enemyHealth_UI.maxValue;
    }

    public void ConfigureImportanceUI(EnemyImportance enemyImportance)
    {
        switch (enemyImportance)
        {
            case EnemyImportance.Normal:
                enemyImportance_UI.text = "";
                return;
            case EnemyImportance.Rare:
                enemyImportance_UI.text = "Rare";
                enemyImportance_UI.color = rareColour;
                return;
            case EnemyImportance.MiniBoss:
                enemyImportance_UI.text = "Miniboss";
                enemyImportance_UI.color = miniBossColour;
                return;
            case EnemyImportance.RareMiniBoss:
                enemyImportance_UI.text = "Rare Miniboss";
                enemyImportance_UI.color = rareMiniBossColour;
                return;
            case EnemyImportance.Boss:
                enemyImportance_UI.text = "Boss";
                enemyImportance_UI.color = bossColour;
                return;
            case EnemyImportance.RareBoss:
                enemyImportance_UI.text = "Rare Boss";
                enemyImportance_UI.color = RareBossColour;
                return;
        }
    }

    public void SetHP(int amount) { enemyHealth_UI.value = amount; }
}

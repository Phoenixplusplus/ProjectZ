using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOverheadUI : MonoBehaviour
{
    [Header("Overhead UI")]
    [SerializeField]
    Color rareColour = Color.white;
    [SerializeField]
    Color miniBossColour = Color.white;
    [SerializeField]
    Color rareMiniBossColour = Color.white;
    [SerializeField]
    Color bossColour = Color.white;
    [SerializeField]
    Color RareBossColour = Color.white;

    [Header("References")]
    [SerializeField]
    GameObject mainCamera = null;
    [SerializeField]
    Text enemyName_UI = null;
    [SerializeField]
    Text enemyImportance_UI = null;
    [SerializeField]
    Slider enemyHealth_UI = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Keep this element facing the camera
        transform.rotation = mainCamera.transform.rotation;
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

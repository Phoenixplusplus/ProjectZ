using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Event to be sent when level Unit has moved successfully
    public delegate void UnitChangeSuccessful();
    public static event UnitChangeSuccessful NewUnitSuccessful;
    // Event to be sent when at the end of level
    public delegate void EndOfLevel();
    public static event EndOfLevel AtEndOfLevel;

    [Header("Level Characteristics")]
    public LevelType levelType;
    public int levelUnits = 0;
    public int moveUnitsByAmount = 1;
    public float unitsMoveTime = 1f;
    public int atUnit = 1;
    public bool endOfLevel = false;

    [Header("Class References")]
    [SerializeField]
    LevelMaker levelMaker = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Events
    void OnEnable()
    {
        EventManager.PassOnUnitIncremented += IncrementLevelAtUnit;
    }
    void OnDisable()
    {
        EventManager.PassOnUnitIncremented -= IncrementLevelAtUnit;
    }
    #endregion

    void IncrementLevelAtUnit()
    {
        if (atUnit < levelUnits)
        {
            atUnit++;
            Debug.Log("LevelManager:: Heared from EventManager that a Unit has been incremented, atUnit now " + atUnit);
            if (NewUnitSuccessful != null) NewUnitSuccessful();
        }
    }

    // Get next enemy spawn positions
    public Vector3[] GetNextEnemySpawnPositions(int amount)
    {
        return levelMaker.GetNextEnemySpawnPositions(amount, atUnit);
    }

    // GameManager calls
    #region GameManager Calls
    public bool MoveLevel()
    {
        if (atUnit < levelUnits)
        {
            levelMaker.MoveLevel(moveUnitsByAmount, unitsMoveTime);
            return true;
        }
        else
        {
            endOfLevel = true;
            Debug.Log("We are at unit " + atUnit + " of a maximum " + levelUnits + " units");
            if (AtEndOfLevel != null) AtEndOfLevel();
            return false;
        }
    }

    public void MakeLevel()
    {
        levelMaker.MakeFloor(levelType, levelUnits);
    }

    public void ResetLevel()
    {
        endOfLevel = false;
        atUnit = 1;
        levelMaker.ResetLevelGeometry();
    }
    #endregion
}

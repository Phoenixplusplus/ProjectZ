using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Characteristics")]
    public LevelType levelType;
    public int levelUnits = 10;
    public int moveUnitsByAmount = 1;
    public float unitsMoveTime = 1f;
    public int atUnit { get; private set; } = 0;

    [Header("Class References")]
    [SerializeField]
    LevelMaker level;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) level.MoveLevel(moveUnitsByAmount, unitsMoveTime);
        if (Input.GetKeyDown(KeyCode.Alpha1)) level.MakeFloor(levelType, levelUnits);
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            atUnit = 0;
            level.ResetLevelGeometry();
        }
    }

    // Events
    void OnEnable()
    {
        EventManager.PassOnUnitIncremented += IncrementAtUnit;
    }
    void OnDisable()
    {
        EventManager.PassOnUnitIncremented -= IncrementAtUnit;
    }
    #endregion

    void IncrementAtUnit() { atUnit++; Debug.Log("Heared from EventManager that a Unit has been incremented, atUnit now " + atUnit); }
}

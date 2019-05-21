using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Create events to be passed on
    public delegate void PassOnUnitChange();
    public static event PassOnUnitChange PassOnUnitIncremented;

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
        LevelMover.UnitIncremented += BroadcastLevelUnitIncremented;
    }
    void OnDisable()
    {
        LevelMover.UnitIncremented -= BroadcastLevelUnitIncremented;
    }
    #endregion

    // Pass events on
    void BroadcastLevelUnitIncremented() { if (PassOnUnitIncremented != null) PassOnUnitIncremented(); Debug.Log("Heared from LevelMover that it has incremented a Unit.. Broadcasting"); }
}

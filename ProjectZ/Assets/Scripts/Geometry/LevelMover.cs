using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    // Create event for incrementing a Unit
    public delegate void UnitChange();
    public static event UnitChange UnitIncremented;

    public float floorLength;

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

    public void StopCoroutines() { StopAllCoroutines(); }
    public void MoveLevel(int amount, float time) { StartCoroutine(MoveLevelIE(amount, time)); }

    IEnumerator MoveLevelIE(int amount, float time)
    {
        float currentTime = 0;
        Vector3 startPos = transform.position;
        Vector3 target = startPos - new Vector3(floorLength * amount, 0, 0);
        float speed = Vector3.Distance(transform.position, target) / time;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        // Send message that unit has been incremented
        if (UnitIncremented != null) UnitIncremented();
        yield break;
    }
}

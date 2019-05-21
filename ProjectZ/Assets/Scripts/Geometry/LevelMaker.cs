using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField]
    LevelMover levelMover;

    [Header("Transform Holders")]
    [SerializeField]
    Transform geometry;

    [Header("Geometry Prefabs")]
    [SerializeField]
    GameObject[] floorPrefabs; // 0 = Grass, 1 = Sand

    [SerializeField]
    List<GameObject> floorList = new List<GameObject>();

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

    public void MakeFloor(LevelType levelType, int amount)
    {
        ResetFloorPosition(); // Reset safeguard
        amount = amount + 1; // Create 1 more unit, so we start at Unit 0

        Vector3 parentPos = geometry.position;
        switch (levelType)
        {
            case LevelType.Grass:
                Vector3 grassFloorBounds = floorPrefabs[0].GetComponent<Renderer>().bounds.size;
                // Tell the LevelMover.cs the length of floor object
                levelMover.floorLength = grassFloorBounds.x;
                for (int i = 0; i < amount; i++)
                {
                    GameObject newFloor = Instantiate(floorPrefabs[0], parentPos + new Vector3(i * grassFloorBounds.x, 0, 0), Quaternion.identity);
                    newFloor.transform.parent = geometry;
                    floorList.Add(newFloor);
                }
                return;
            case LevelType.Sand:
                Vector3 sandFloorBounds = floorPrefabs[1].GetComponent<Renderer>().bounds.size;
                // Tell the LevelMover.cs the length of floor object
                levelMover.floorLength = sandFloorBounds.x;
                for (int i = 0; i < amount; i++)
                {
                    GameObject newFloor = Instantiate(floorPrefabs[1], parentPos + new Vector3(i * sandFloorBounds.x, 0, 0), Quaternion.identity);
                    newFloor.transform.parent = geometry;
                    floorList.Add(newFloor);
                }
                return;
            default: return;
        }
    }

    public void MoveLevel(int amount, float time)
    {
        levelMover.MoveLevel(amount, time);
    }

    public void DestroyFloor()
    {
        foreach (GameObject floorPiece in floorList)
        {
            Destroy(floorPiece);
        }
        floorList.Clear();
    }

    public void ResetFloorPosition()
    {
        levelMover.transform.position = Vector3.zero;
        levelMover.floorLength = 0;
    }

    public void ResetLevelGeometry()
    {
        levelMover.StopCoroutines();
        DestroyFloor();
        ResetFloorPosition();
    }
}

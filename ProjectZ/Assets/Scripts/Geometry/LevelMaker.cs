using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField]
    LevelMover levelMover = null;

    [Header("Transform Holders")]
    [SerializeField]
    Transform geometry = null;

    [Header("Geometry Prefabs")]
    [SerializeField]
    FloorPrefabs floorPrefabs;

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
                Vector3 grassFloorBounds = floorPrefabs.grassFloor.GetComponent<Renderer>().bounds.size;
                // Tell the LevelMover.cs the real dimenstions of floor object
                levelMover.floorLength = grassFloorBounds.x;
                levelMover.floorWidth = grassFloorBounds.z;
                levelMover.floorHeight = grassFloorBounds.y;
                for (int i = 0; i < amount; i++)
                {
                    GameObject newFloor = Instantiate(floorPrefabs.grassFloor, parentPos + new Vector3(i * grassFloorBounds.x, 0, 0), Quaternion.identity);
                    newFloor.transform.parent = geometry;
                    floorList.Add(newFloor);
                }
                return;
            case LevelType.Sand:
                Vector3 sandFloorBounds = floorPrefabs.sandFloor.GetComponent<Renderer>().bounds.size;
                levelMover.floorLength = sandFloorBounds.x;
                levelMover.floorWidth = sandFloorBounds.z;
                levelMover.floorHeight = sandFloorBounds.y;
                for (int i = 0; i < amount; i++)
                {
                    GameObject newFloor = Instantiate(floorPrefabs.sandFloor, parentPos + new Vector3(i * sandFloorBounds.x, 0, 0), Quaternion.identity);
                    newFloor.transform.parent = geometry;
                    floorList.Add(newFloor);
                }
                return;
            default: return;
        }
    }

    // Move geometry behind player to simulate movement
    public void MoveLevel(int amount, float time)
    {
        levelMover.MoveLevel(amount, time);
    }

    // Clean up spawned geomatry
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

    // Get next enemy spawn positions
    public Vector3[] GetNextEnemySpawnPositions(int amount, int atUnit)
    {
        Vector3[] enemyPositions = new Vector3[amount];
        if (atUnit < floorList.Count)
        {
            Vector3 spawnNegativeZ = floorList[atUnit].transform.position - new Vector3(0, 0, levelMover.floorWidth / 2); // Start spawn position at one side of the Z
            float margin = (levelMover.floorWidth / amount) / 2; // Create a margin of half the spawn distance, on each edge
            for (int i = 0; i < amount; i++)
            {
                enemyPositions[i] = spawnNegativeZ + new Vector3(0, 0, ((levelMover.floorWidth / amount) * i) + margin);
            }
            return enemyPositions;
        }
        else
        {
            Debug.Log("LevelMaker:: At the end of the level!");
            return enemyPositions;
        }
    }
}

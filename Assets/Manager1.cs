using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager1 : MonoBehaviour
{

    public GameObject[] prefabArray;
    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 10.0f;
    private int amountOfTiles = 7;
    private List<GameObject> activeTiles;
    private float safeZone = 15.0f;
    private int lastPrefabIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for (int i=0;i<amountOfTiles;i++)
        {
            if(i < 2)
                spawnTile(0);
            else 
                spawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > spawnZ - amountOfTiles * tileLength)
        {
            spawnTile();
            deleteTile();
        }
    }

    private void spawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
            go = Instantiate(prefabArray[randomPrefabIndex()]) as GameObject;
        } else
        {
            go = Instantiate(prefabArray[lastPrefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

    private int randomPrefabIndex()
    {
        if(prefabArray.Length <= 1)
        {
            return 0;
        } else
        {
            int randomIndex = lastPrefabIndex;
            while(randomIndex == lastPrefabIndex)
            {
                randomIndex = Random.Range(0, prefabArray.Length);
            }
            lastPrefabIndex = randomIndex;
            return randomIndex;
        }
    }
}

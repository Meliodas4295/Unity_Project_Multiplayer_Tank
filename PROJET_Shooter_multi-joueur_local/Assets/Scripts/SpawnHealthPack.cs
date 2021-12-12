using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthPack : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPackPrefab;

    private float spawnRange = 40f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);
            Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);
            Instantiate(healthPackPrefab, randomPos, healthPackPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

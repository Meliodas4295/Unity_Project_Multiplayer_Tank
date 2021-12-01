using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject mine = Instantiate(minePrefab, transform.position, transform.rotation);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTank : MonoBehaviour
{
    [SerializeField]
    private GameObject tankPrefab;
    private List<Vector3> positionTank = new List<Vector3>();// = [new Vector3(0, 0, 0), new Vector3(50, 0, 0)];
    private List<Quaternion> rotationTank = new List<Quaternion>();
    //private List<GameObject> tanksPrefab;
    // Start is called before the first frame update
    void Start()
    {
        positionTank.Add(new Vector3(0, 0, 0));
        positionTank.Add(new Vector3(0, 0, 20));
        rotationTank.Add(Quaternion.Euler(0, 0, 0));
        rotationTank.Add(Quaternion.Euler(0, 180, 0));
        for (int i = 1; i < 3; i++)
        {
            GameObject tank = Instantiate(tankPrefab, positionTank[i - 1], rotationTank[i - 1]);
            tank.GetComponent<TankManager>().healthBar = GameObject.Find("HealthBarJ"+i).GetComponent<HealthBar>();
            tank.GetComponent<TankManager>().idTank = i;
            if (i == 1)
            {
                tank.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
            else
            {
                tank.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

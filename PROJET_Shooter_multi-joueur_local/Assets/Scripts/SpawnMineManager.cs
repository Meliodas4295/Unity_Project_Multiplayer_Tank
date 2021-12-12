using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minePrefab;
    private Vector3 tankPosition;
    private float distanceMineTank = 0;
    private List<GameObject> mine = new List<GameObject>();
    private List<float> timeBeforeActivation = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mine.Add(Instantiate(minePrefab, transform.position, transform.rotation));
            timeBeforeActivation.Add(Time.time);
        }
        tankPosition = gameObject.GetComponentInParent<Transform>().position;
        for (int i = 0; i < mine.Count; i++)
        {
            if (Time.time > timeBeforeActivation[i] + 5f)
            {
                distanceMineTank = Mathf.Sqrt(Mathf.Pow(mine[i].transform.position.x - tankPosition.x, 2) + Mathf.Pow(mine[i].transform.position.y - tankPosition.y, 2) + Mathf.Pow(mine[i].transform.position.z - tankPosition.z, 2));
                //Debug.Log(distanceMineTank);
                if (distanceMineTank < 5)
                {
                    Destroy(mine[i]);
                    mine.Remove(mine[i]);
                    timeBeforeActivation.Remove(timeBeforeActivation[i]);
                    gameObject.GetComponentInParent<TankManager>().SetDamageToTank(0.1f);
                }
            }
        }
    }

}

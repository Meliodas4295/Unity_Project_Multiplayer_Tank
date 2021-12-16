using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minePrefab;
    public float timeBeforeActivation;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<TankManager>().numberMine != 0)
        {
            if (Input.GetKeyDown(PlayerBouton()))
            {
                GameObject mine = Instantiate(minePrefab, transform.position, transform.rotation);
                timeBeforeActivation = Time.time;
                gameObject.GetComponentInParent<TankManager>().numberMine -= 1; 
            }
        }
    }
    KeyCode PlayerBouton()
    {
        if (gameObject.GetComponentInParent<TankManager>().idTank == 1)
        {
            return KeyCode.M;
        }
        else
        {
            return KeyCode.R;
        }

    }


}


/*tankPosition = gameObject.GetComponentInParent<Transform>().position;
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
       }*/

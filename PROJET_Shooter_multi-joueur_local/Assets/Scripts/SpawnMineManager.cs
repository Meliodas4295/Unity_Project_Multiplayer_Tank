using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minePrefab;
    private float timeBeforeActivation;
    private TankManager tankManager;

    public float GetTimeBeforeActivation()
    {
        return this.timeBeforeActivation;
    }
    public void SetTimeBeforeActivation(float timeBeforeActivation)
    {
        this.timeBeforeActivation = timeBeforeActivation;
    }
    void Start()
    {
        tankManager = gameObject.GetComponentInParent<TankManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tankManager.GetIsGameOver())
        {
            CreateMine();
        }
    }

    private void CreateMine()
    {
        if (tankManager.GetNumberMine() != 0)
        {
            if (Input.GetKeyDown(PlayerBouton()))
            {
                GameObject mine = Instantiate(minePrefab, transform.position, transform.rotation);
                timeBeforeActivation = Time.time;
                tankManager.SetNumberMine(tankManager.GetNumberMine() - 1);
            }
        }
    }

    KeyCode PlayerBouton()
    {
        if (tankManager.GetIdTank() == 1)
        {
            return KeyCode.M;
        }
        else
        {
            return KeyCode.R;
        }

    }


}

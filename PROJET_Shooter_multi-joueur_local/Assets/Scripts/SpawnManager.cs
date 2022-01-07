using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    enum Packs { Mine, Munition, Health};
    enum PowerUp { Speed, Shield, UnlimitedAmmunition };
    [SerializeField]
    private GameObject minePackPrefab;
    [SerializeField]
    private GameObject munitionPackPrefab;
    [SerializeField]
    private GameObject healthPackPrefab;
    [SerializeField]
    private GameObject tankPrefab;
    [SerializeField]
    private GameObject speedPrefab;
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private GameObject unlimitedAmmunitionPrefab;

    private NavMeshHit hit;

    private int randomPowerUp;
    private List<Vector3> positionTank = new List<Vector3>();
    private List<Quaternion> rotationTank = new List<Quaternion>();

    private float spawnRange = 40f * 3;
    private int randomPack;

    private float timerPack;
    private float timerPU;

    private bool isGameOver = false;

    public List<Vector3> GetPositionTank()
    {
        return this.positionTank;
    }
    public void SetPositionTank(List<Vector3> positionTank)
    {
        this.positionTank = positionTank;
    }
    public List<Quaternion> GetRotationTank()
    {
        return this.rotationTank;
    }
    public void SetRotationTank(List<Quaternion> rotationTank)
    {
        this.rotationTank = rotationTank;
    }
    public bool GetIsGameOver()
    {
        return this.isGameOver;
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTankPositionAndRotation();
        InstantiateTank();
        InstantiatePack();
        InstantiatePowerUp();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            timerPack += Time.deltaTime;
            timerPU += Time.deltaTime;
            if (timerPack > 30)
            {
                InstantiatePack();
                timerPack = 0;
            }
            if (timerPU > 60)
            {
                InstantiatePowerUp();
                timerPU = 0;
            }
        }
    }

    private void InstantiatePowerUp()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosShield, randomPosSpeed, randomPosUnlimitedAmmunition;
            RandomSpawnPositionPowerUp(out randomPosShield, out randomPosSpeed, out randomPosUnlimitedAmmunition);
            if (randomPowerUp == ((int)PowerUp.Speed))
            {
                if (NavMesh.SamplePosition(randomPosSpeed, out hit, 15, NavMesh.AllAreas))
                {
                    Instantiate(speedPrefab, hit.position, speedPrefab.transform.rotation);
                }
            }
            else if (randomPowerUp == ((int)PowerUp.Shield))
            {
                if (NavMesh.SamplePosition(randomPosShield, out hit, 15, NavMesh.AllAreas))
                {
                    Instantiate(shieldPrefab, randomPosShield, shieldPrefab.transform.rotation);
                }
            }
            else if (randomPowerUp == ((int)PowerUp.UnlimitedAmmunition))
            {
                if (NavMesh.SamplePosition(randomPosUnlimitedAmmunition, out hit, 15, NavMesh.AllAreas))
                {
                    Instantiate(unlimitedAmmunitionPrefab, randomPosUnlimitedAmmunition, unlimitedAmmunitionPrefab.transform.rotation);
                }
            }
        }
    }

    private void RandomSpawnPositionPowerUp(out Vector3 randomPosShield, out Vector3 randomPosSpeed, out Vector3 randomPosUnlimitedAmmunition)
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        randomPosShield = new Vector3(spawnPosX, 2.5f, spawnPosZ);
        randomPosSpeed = new Vector3(spawnPosX, 1.5f, spawnPosZ);
        randomPosUnlimitedAmmunition = new Vector3(spawnPosX, 1.5f, spawnPosZ);
        randomPowerUp = Random.Range(0, 3);
    }

    private void InstantiatePack()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPos = RandomSpawnPositionPack();
            if (NavMesh.SamplePosition(randomPos, out hit, 15, NavMesh.AllAreas))
            {
                InstantiateMunitionPack();
                InstantiateHealthPack();
                InstantiateMinePack();
            }

        }
    }

    private Vector3 RandomSpawnPositionPack()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);
        randomPack = Random.Range(0, 3);
        return randomPos;
    }

    private void InstantiateMinePack()
    {
        if (randomPack == (int)Packs.Mine)
        {
            GameObject minePack = Instantiate(minePackPrefab, hit.position, minePackPrefab.transform.rotation);
        }
    }

    private void InstantiateHealthPack()
    {
        if (randomPack == (int)Packs.Health)
        {
            GameObject healthPack = Instantiate(healthPackPrefab, hit.position, healthPackPrefab.transform.rotation);
        }
    }

    private void InstantiateMunitionPack()
    {
        if (randomPack == (int)Packs.Munition)
        {
            GameObject munition = Instantiate(munitionPackPrefab, hit.position, munitionPackPrefab.transform.rotation);
        }
    }

    private void InstantiateTank()
    {
        for (int i = 1; i < 3; i++)
        {
            GameObject tank = Instantiate(tankPrefab, positionTank[i - 1], rotationTank[i - 1]);
            tank.name = "Tank" + i;
            TankManager tankManager = tank.GetComponent<TankManager>();
            tankManager.SetHealthBar(GameObject.Find("HealthBar/HealthBarJ" + i).GetComponent<HealthBar>());
            tankManager.SetPullCharge(GameObject.Find("PullCharge" + i).GetComponent<PullCharge>());
            tankManager.SetIdTank(i);
            tankManager.SetNumberOfShell(GameObject.Find("NumberOfShell" + i).GetComponent<TextMeshProUGUI>());
            tankManager.SetNumberOfMine(GameObject.Find("NumberOfMine" + i).GetComponent<TextMeshProUGUI>());
            SplitScreenCamera(tank, tankManager);
        }
    }

    public void SplitScreenCamera(GameObject tank, TankManager tankManger)
    {
        if (tankManger.GetIdTank() == 1)
        {
            tank.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        }
        else
        {
            tank.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
    }

    public void StartTankPositionAndRotation()
    {
        positionTank.Add(new Vector3(120, 0, -120));
        positionTank.Add(new Vector3(-100, 0, 110));
        rotationTank.Add(Quaternion.Euler(0, 0, 0));
        rotationTank.Add(Quaternion.Euler(0, 180, 0));
    }
}

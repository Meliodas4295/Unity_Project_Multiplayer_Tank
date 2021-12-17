using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject spawnManager;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject gameOverUI;
    public TextMeshProUGUI winnerPlayer;

    private bool IsEndGame = false;
    private bool IsPlayed;

    private List<GameObject> tanks = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        StartCoroutine(FindTank());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckHealthBar());
        if (IsEndGame)
        {
            for(int i = 0; i < tanks.Count; i++)
            {
                tanks[i].GetComponent<TankManager>().SetIsGameOver(true);
                if (tanks[i].GetComponent<TankManager>().GetIsWinner())
                {
                    gameOverUI.SetActive(true);
                    tanks[i].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1f, 1);
                    tanks[i].transform.Find("PivotCamera").transform.Rotate(new Vector3(0, 1, 0)*0.1f);
                    winnerPlayer.text = tanks[i].name;
                }
                else
                {
                    tanks[i].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0, 1);
                }
            }
            spawnManager.GetComponent<SpawnManager>().SetIsGameOver(true);

        }
    }

    public void SetButtonPlay()
    {
        spawnManager.SetActive(true);
        healthBar.SetActive(true);
        menu.SetActive(false);
    }

    public void SetButtonQuit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        IsEndGame = false;
        for(int i = 0; i < tanks.Count; i++)
        {
            tanks[i].GetComponent<TankManager>().SetIsGameOver(false);
            tanks[i].GetComponent<TankManager>().healthBar.GetComponent<Scrollbar>().size = 1;
            tanks[i].transform.position = spawnManager.GetComponent<SpawnManager>().positionTank[i];
            tanks[i].transform.rotation = spawnManager.GetComponent<SpawnManager>().rotationTank[i];
            tanks[i].transform.Find("PivotCamera").transform.rotation = Quaternion.Euler(0, 0, 0);

            spawnManager.GetComponent<SpawnManager>().SplitScreenCamera(tanks[i], tanks[i].GetComponent<TankManager>());
            if (!tanks[i].GetComponent<TankManager>().GetIsWinner())
            {
                tanks[i].GetComponent<TankManager>().SetIsWinner(true);
            }
        }
        spawnManager.GetComponent<SpawnManager>().SetIsGameOver(false);
    }

    IEnumerator FindTank()
    {
        yield return new WaitUntil(() => spawnManager.activeSelf);
        tanks.Add(GameObject.Find("Tank1"));
        tanks.Add(GameObject.Find("Tank2"));
    }

    IEnumerator CheckHealthBar()
    {
        yield return new WaitUntil(() => healthBar.activeSelf);
        for(int i = 1; i< tanks.Count+1; i++)
        {
            if (GameObject.Find("HealthBar/HealthBarJ" + i).GetComponent<Scrollbar>().size == 0)
            {
                IsEndGame = true;
                tanks[i - 1].GetComponent<TankManager>().SetIsWinner(false);
            }
        }
    }
}


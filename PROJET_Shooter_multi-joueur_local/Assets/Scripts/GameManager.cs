using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Button button;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject spawnManager;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject optionMenu;
    [SerializeField]
    private TextMeshProUGUI winnerPlayer;

    private bool IsEndGame = false;

    private List<GameObject> tanks = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                TurnCameraAroundWinnerPlayer(i);
            }
            spawnManager.GetComponent<SpawnManager>().SetIsGameOver(true);

        }
    }

    private void TurnCameraAroundWinnerPlayer(int i)
    {
        if (tanks[i].GetComponent<TankManager>().GetIsWinner())
        {
            healthBar.SetActive(false);
            gameOverUI.SetActive(true);
            tanks[i].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1f, 1);
            tanks[i].transform.Find("PivotCamera").transform.Rotate(new Vector3(0, 1, 0) * 0.1f);
            winnerPlayer.text = tanks[i].name;
        }
        else
        {
            tanks[i].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0, 1);
        }
    }
    public void SetButtonOption()
    {
        menu.SetActive(false);
        optionMenu.SetActive(true);

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

    public void ReturnOptionToMenu()
    {
        menu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Reset()
    {
        IsEndGame = false;
        healthBar.SetActive(true);
        for(int i = 0; i < tanks.Count; i++)
        {
            tanks[i].GetComponent<TankManager>().SetIsGameOver(false);
            tanks[i].GetComponent<TankManager>().GetHealthBar().GetComponent<Scrollbar>().size = 1;
            tanks[i].transform.position = spawnManager.GetComponent<SpawnManager>().GetPositionTank()[i];
            tanks[i].transform.rotation = spawnManager.GetComponent<SpawnManager>().GetRotationTank()[i];
            tanks[i].transform.Find("PivotCamera").transform.rotation = Quaternion.Euler(0, 0, 0);
            tanks[i].GetComponent<TankManager>().SetNumberMunition(3);

            spawnManager.GetComponent<SpawnManager>().SplitScreenCamera(tanks[i], tanks[i].GetComponent<TankManager>());
            if (!tanks[i].GetComponent<TankManager>().GetIsWinner())
            {
                tanks[i].transform.Find("PivotCamera").transform.rotation = Quaternion.Euler(0, 180, 0);
                tanks[i].GetComponent<TankManager>().SetIsWinner(true);
                tanks[i].GetComponent<TankManager>().GetHealthBar().GetSprite().GetComponent<Image>().color = new Color(0, 128, 0);
            }
        }
        spawnManager.GetComponent<SpawnManager>().SetIsGameOver(false);
        gameOverUI.SetActive(false);
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
        CheckEndGameMoment();
    }

    private void CheckEndGameMoment()
    {
        for (int i = 1; i < tanks.Count + 1; i++)
        {
            if (healthBar.activeSelf)
            {
                if (GameObject.Find("HealthBar/HealthBarJ" + i).GetComponent<Scrollbar>().size == 0)
                {
                    IsEndGame = true;
                    tanks[i - 1].GetComponent<TankManager>().SetIsWinner(false);
                }
            }
        }
    }
}


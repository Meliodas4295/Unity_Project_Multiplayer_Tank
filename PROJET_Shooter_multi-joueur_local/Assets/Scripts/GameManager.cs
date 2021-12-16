using System.Collections;
using System.Collections.Generic;
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

    bool GameOver()
    {
        return IsEndGame;
    }

    IEnumerator FindTank()
    {
        yield return new WaitUntil(() => spawnManager.activeSelf);
        tanks.Add(GameObject.Find("Tank1"));
        tanks.Add(GameObject.Find("Tank2"));
        Debug.Log("Ok");
    }
}


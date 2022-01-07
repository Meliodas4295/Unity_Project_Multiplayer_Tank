using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject sprite;
    private bool IsGameOver = false;
    private Color goodColor = new Color(0, 128, 0);
    private Color middleColor = new Color(255, 165, 0);
    private Color badColor = new Color(255, 0, 0);
    // Start is called before the first frame update

    public bool GetIsGameOver()
    {
        return this.IsGameOver;
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this.IsGameOver = isGameOver;
    }

    public GameObject GetHealthBar()
    {
        return this.healthBar;
    }

    public void SetHealthBar(GameObject healthBar)
    {
        this.healthBar = healthBar;
    }

    public GameObject GetSprite()
    {
        return this.sprite;
    }

    public void SetSprite(GameObject sprite)
    {
        this.sprite = sprite;
    }

    void Start()
    {
        sprite.GetComponent<Image>().color = goodColor;
    }

    public void SetDamages(float value)
    {
        healthBar.GetComponent<Scrollbar>().size -= value;

        float totalValue = healthBar.GetComponent<Scrollbar>().size;

        SetColor(totalValue);
    }

    void SetColor(float value)
    {
        if(value >= 0.5f)
        {
            sprite.GetComponent<Image>().color = goodColor;
        }
        else if(value >= 0.25f && value < 0.5f)
        {
            sprite.GetComponent<Image>().color = middleColor;
        }
        else
        {
            sprite.GetComponent<Image>().color = badColor;
        }
    }
}

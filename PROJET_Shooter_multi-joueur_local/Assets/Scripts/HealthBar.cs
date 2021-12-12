using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject sprite;
    private Color goodColor = new Color(0, 128, 0);
    private Color middleColor = new Color(255, 165, 0);
    private Color badColor = new Color(255, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        sprite.GetComponent<Image>().color = goodColor;
    }

    // Update is called once per frame
    void Update()
    {

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

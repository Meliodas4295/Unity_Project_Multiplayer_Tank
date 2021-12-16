using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullCharge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Scrollbar>().size = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Charge(float value)
    {
        gameObject.GetComponent<Scrollbar>().size = value;
        if (value > 1)
        {
            gameObject.GetComponent<Scrollbar>().size = value;
        }
    }
}

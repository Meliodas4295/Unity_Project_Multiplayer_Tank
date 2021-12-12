using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<TankManager>().SetDamageToTank(0.1f);
        }
        Destroy(gameObject);
    }
}

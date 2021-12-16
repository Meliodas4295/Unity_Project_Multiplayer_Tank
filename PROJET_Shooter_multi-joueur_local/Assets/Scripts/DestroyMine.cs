using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > other.GetComponentInChildren<SpawnMineManager>().timeBeforeActivation + 5f)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(gameObject, 0.5f);
                other.gameObject.GetComponent<TankManager>().SetDamageToTank(0.1f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMine : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > other.GetComponentInChildren<SpawnMineManager>().GetTimeBeforeActivation() + 5f)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(gameObject, 0.5f);
                audioSource.PlayOneShot(audioSource.clip);
                other.gameObject.GetComponent<TankManager>().SetDamageToTank(0.3f);
            }
        }
    }
}

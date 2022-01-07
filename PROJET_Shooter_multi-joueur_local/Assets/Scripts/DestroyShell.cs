using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShell : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<TankManager>().SetDamageToTank(0.3f);
        }
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        Destroy(gameObject,0.5f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}

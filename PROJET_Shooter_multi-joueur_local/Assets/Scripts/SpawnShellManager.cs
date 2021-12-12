using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shellPrefab;
    public float timePressed;
    public float resetMunitionTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressedTimer();
    }

    void KeyPressedTimer()
    {
        if (gameObject.GetComponentInParent<TankManager>().numberMunition != 0)
        {
            if (Input.GetKeyDown(PlayerBouton()))
            {
                timePressed = Time.time;
            }

            if (Input.GetKeyUp(PlayerBouton()))
            {
                timePressed = Time.time - timePressed;
                ShellManager(timePressed * 20);
                gameObject.GetComponentInParent<TankManager>().numberMunition -= 1;
                Debug.Log(gameObject.GetComponentInParent<TankManager>().numberMunition);
                if(gameObject.GetComponentInParent<TankManager>().numberMunition == 0)
                {
                    resetMunitionTime = Time.time;
                }
            }
        }
    }

    void ShellManager(float power)
    {
        GameObject shell = Instantiate(shellPrefab, transform.position, transform.rotation);
        shell.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
    }

    KeyCode PlayerBouton()
    {
        if(gameObject.GetComponentInParent<TankManager>().idTank == 1)
        {
            return KeyCode.Space;
        }
        else
        {
            return KeyCode.F;
        }

    }
}

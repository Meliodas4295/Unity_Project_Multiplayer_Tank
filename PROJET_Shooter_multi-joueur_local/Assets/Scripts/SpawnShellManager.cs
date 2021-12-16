using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shellPrefab;
    public float timePressed;
    private float timeDrop;
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
            if (Input.GetKey(PlayerBouton()))
            {
                timeDrop += Time.deltaTime;
                gameObject.GetComponentInParent<TankManager>().pullCharge.GetComponent<PullCharge>().Charge(timeDrop/10);
            }

            if (Input.GetKeyUp(PlayerBouton()))
            {
                timeDrop = 0;
                gameObject.GetComponentInParent<TankManager>().pullCharge.GetComponent<PullCharge>().Charge(timeDrop);
                timePressed = (Time.time - timePressed)+1;
                ShellManager(timePressed * 40);
                gameObject.GetComponentInParent<TankManager>().numberMunition -= 1;
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

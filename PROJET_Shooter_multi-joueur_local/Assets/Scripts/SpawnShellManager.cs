using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shellPrefab;
    private float timePressed;
    private float timeDrop;
    private float resetMunitionTime;
    private AudioSource audioSource;
    private TankManager tankManager;

    public float GetTimePressed()
    {
        return this.timePressed;
    }
    public void SetTimePressed(float timePressed)
    {
        this.timePressed = timePressed;
    }
    public float GetResetMunitionTime()
    {
        return this.resetMunitionTime;
    }
    public void SetResetMunitionTime(float resetMunitionTime)
    {
        this.resetMunitionTime = resetMunitionTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        tankManager = gameObject.GetComponentInParent<TankManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tankManager.GetIsGameOver())
        {
            KeyPressedTimer();
        }
    }

    void KeyPressedTimer()
    {
        if (tankManager.GetNumberMunition() != 0 || tankManager.GetIsUnlimitedMunition())
        {
            if (Input.GetKeyDown(PlayerBouton()))
            {
                timePressed = Time.time;
            }
            if (Input.GetKey(PlayerBouton()))
            {
                timeDrop += Time.deltaTime;
                tankManager.GetPullCharge().GetComponent<PullCharge>().Charge(timeDrop/10);
            }

            if (Input.GetKeyUp(PlayerBouton()))
            {
                audioSource.PlayOneShot(audioSource.clip);
                timeDrop = 0;
                tankManager.GetPullCharge().GetComponent<PullCharge>().Charge(timeDrop);
                timePressed = (Time.time - timePressed)+1;
                ShellManager(timePressed * 40);
                tankManager.SetNumberMunition(tankManager.GetNumberMunition() - 1);
                if(tankManager.GetNumberMunition() == 0)
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
        if(tankManager.GetIdTank() == 1)
        {
            return KeyCode.Space;
        }
        else
        {
            return KeyCode.F;
        }

    }
}

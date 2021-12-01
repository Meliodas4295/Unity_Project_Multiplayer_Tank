using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shellPrefab;
    private float timePressed;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timePressed = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            timePressed = Time.time - timePressed;
            switch ((int)timePressed)
            {
                case 0:
                    ShellManager(10);
                    break;
                case 1:
                    ShellManager(20);
                    break;
                case 2:
                    ShellManager(30);
                    break;
                default:
                    ShellManager(30);
                    break;
            }
        }
    }

    void ShellManager(int power)
    {
        GameObject shell = Instantiate(shellPrefab, transform.position, transform.rotation);
        //shellPrefabs.Add(shellPrefab);
        shell.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
    }
}

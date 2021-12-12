using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    [SerializeField]
    private float speedTranslation;
    [SerializeField]
    private float speedRotation;
    public bool isLaunch = false;
    public HealthBar healthBar;
    public int idTank;
    public int numberMunition = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"+idTank);
        float horizontalInput = Input.GetAxis("Horizontal"+idTank);
        transform.Translate(Vector3.forward * verticalInput * speedTranslation * Time.deltaTime);
        transform.Rotate(new Vector3(0,1,0) * horizontalInput * speedRotation * Time.deltaTime);
        if(numberMunition == 0)
        {
            if (Time.time > gameObject.GetComponentInChildren<SpawnShellManager>().resetMunitionTime + 5f)
            {
                Debug.Log("ok munition reset");
                numberMunition = 3;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealthPack"))
        {
            Destroy(collision.gameObject);
            healthBar.GetComponent<Scrollbar>().size = 1;
        }
        if (collision.gameObject.CompareTag("MunitionTag"))
        {
            Destroy(collision.gameObject);
            numberMunition += 1; 
        }
    }

    public void SetDamageToTank(float value)
    {
        healthBar.SetDamages(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPowerUpPrefab;

    private GameObject shieldBonus;

    private Rigidbody tankRb;
    private float speedTranslation = 30;
    private float speedRotation = 5;
    public bool isLaunch = false;
    public HealthBar healthBar;
    public PullCharge pullCharge;
    public int idTank;
    private Vector2 movement;
    public int numberMunition = 3;
    public int numberMine = 3;
    public TextMeshProUGUI numberOfShell;
    public TextMeshProUGUI numberOfMine;
    private float shieldBonusTimer;
    public bool IsGameOver = false;


    void Start()
    {
        tankRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(shieldBonus != null)
        {
            shieldBonusTimer += Time.deltaTime;
            shieldBonus.transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            shieldBonus.transform.Rotate(new Vector3(0, 1, 0) * 0.7f);
        }
        if(shieldBonusTimer > 10)
        {
            Destroy(shieldBonus);
            shieldBonusTimer = 0;
        }
        movement = new Vector2(Input.GetAxis("Horizontal" + idTank), Input.GetAxis("Vertical" + idTank));
        numberOfShell.text = "" + numberMunition;
        numberOfMine.text = "" + numberMine;
    }

    private void FixedUpdate()
    {
        if (!IsGameOver)
        {
            tankRb.velocity = transform.forward * movement.y * speedTranslation;
            tankRb.angularVelocity = new Vector3(0, 1, 0) * movement.x * speedRotation;
            //tankRb.MovePosition(tankRb.position + (transform.forward * movement.y * speedTranslation * Time.deltaTime));
            //transform.Translate(Vector3.forward * verticalInput * speedTranslation * Time.deltaTime);
            //transform.Rotate(new Vector3(0, 1, 0) * movement.x * speedRotation * Time.deltaTime);
            if (numberMunition == 0)
            {
                if (Time.time > gameObject.GetComponentInChildren<SpawnShellManager>().resetMunitionTime + 5f)
                {
                    Debug.Log("ok munition reset");
                    numberMunition = 3;
                }
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
        if (collision.gameObject.CompareTag("MunitionPack"))
        {
            Destroy(collision.gameObject);
            numberMunition += 1; 
        }
        if (collision.gameObject.CompareTag("MinePack"))
        {
            Destroy(collision.gameObject);
            numberMine += 1;
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            if (shieldBonus == null)
            {
                shieldBonus = Instantiate(shieldPowerUpPrefab, transform.position, transform.rotation);
            }
            else
            {
                shieldBonusTimer = 0;
            }

        }
        if (collision.gameObject.CompareTag("ThunderBolt"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void SetDamageToTank(float value)
    {
        if (shieldBonus == null)
        {
            healthBar.SetDamages(value);
        }
    }
}

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
    private float shieldBonusTimer;

    private float UnlimitedAmmunitionBonusTimer;

    private bool isThunderBoltBonus = false;
    private float thunderboltBonusTimer;

    [SerializeField]
    private ParticleSystem speedParticle;

    private Rigidbody tankRb;
    private float speedTranslation = 30;
    private float speedRotation = 2;
    private HealthBar healthBar;
    private PullCharge pullCharge;
    private int idTank;
    private Vector2 movement;
    private int numberMunition = 3;
    private int numberMine = 3;
    private TextMeshProUGUI numberOfShell;
    private TextMeshProUGUI numberOfMine;
    private bool isGameOver = false;
    private bool isWinner = true;
    private AudioSource audioSource;
    private bool isUnlimitedMunition = false;

    public TextMeshProUGUI GetNumberOfShell()
    {
        return this.numberOfShell;
    }
    public void SetNumberOfShell(TextMeshProUGUI numberOfShell)
    {
        this.numberOfShell = numberOfShell;
    }
    public TextMeshProUGUI GetNumberOfMine()
    {
        return this.numberOfMine;
    }
    public void SetNumberOfMine(TextMeshProUGUI numberOfMine)
    {
        this.numberOfMine = numberOfMine;
    }
    public int GetIdTank()
    {
        return this.idTank;
    }
    public void SetIdTank(int idTank)
    {
        this.idTank = idTank;
    }
    public int GetNumberMunition()
    {
        return this.numberMunition;
    }
    public void SetNumberMunition(int numberMunition)
    {
        this.numberMunition = numberMunition;
    }
    public int GetNumberMine()
    {
        return this.numberMine;
    }
    public void SetNumberMine(int numberMine)
    {
        this.numberMine = numberMine;
    }
    public PullCharge GetPullCharge()
    {
        return this.pullCharge;
    }

    public void SetPullCharge(PullCharge pullCharge)
    {
        this.pullCharge = pullCharge;
    }
    public HealthBar GetHealthBar()
    {
        return this.healthBar;
    }

    public void SetHealthBar(HealthBar healthBar)
    {
        this.healthBar = healthBar;
    }
    public bool GetIsUnlimitedMunition()
    {
        return this.isUnlimitedMunition;
    }

    public void SetIsUnlimitedMunition(bool isUnlimitedMunition)
    {
        this.isUnlimitedMunition = isUnlimitedMunition;
    }

    public bool GetIsGameOver()
    {
        return this.isGameOver;
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    public bool GetIsWinner()
    {
        return this.isWinner;
    }

    public void SetIsWinner(bool isWinner)
    {
        this.isWinner = isWinner;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        tankRb = gameObject.GetComponent<Rigidbody>();
        speedParticle.Stop();
    }

    void Update()
    {
        ShieldBonusManager();
        ThunderBoltBonusManager();
        UnlimitedAmmunitionBonusManager();
        movement = new Vector2(Input.GetAxis("Horizontal" + idTank), Input.GetAxis("Vertical" + idTank));
        if (isUnlimitedMunition)
        {
            numberOfShell.text = "∞";
        }
        else
        {
            numberOfShell.text = "" + numberMunition;
        }
        numberOfMine.text = "" + numberMine;
    }

    private void ThunderBoltBonusManager()
    {
        if (isThunderBoltBonus)
        {
            thunderboltBonusTimer += Time.deltaTime;
        }
        if (thunderboltBonusTimer > 10)
        {
            isThunderBoltBonus = false;
            speedTranslation = 30;
            speedParticle.Stop();
        }
    }

    private void UnlimitedAmmunitionBonusManager()
    {
        if (isUnlimitedMunition)
        {
            UnlimitedAmmunitionBonusTimer += Time.deltaTime;
        }
        if (UnlimitedAmmunitionBonusTimer > 10)
        {
            isUnlimitedMunition = false;
            UnlimitedAmmunitionBonusTimer = 0;
            numberMunition = 3;
        }
    }

    private void ShieldBonusManager()
    {
        if (shieldBonus != null)
        {
            shieldBonusTimer += Time.deltaTime;
            shieldBonus.transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            shieldBonus.transform.Rotate(new Vector3(0, 1, 0) * 0.7f);
        }
        if (shieldBonusTimer > 10)
        {
            Destroy(shieldBonus);
            shieldBonusTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            tankRb.velocity = transform.forward * movement.y * speedTranslation;
            tankRb.angularVelocity = new Vector3(0, 1, 0) * movement.x * speedRotation;
            if (numberMunition == 0)
            {
                if (Time.time > gameObject.GetComponentInChildren<SpawnShellManager>().GetResetMunitionTime() + 5f)
                {
                    numberMunition = 3;
                }
            }
            MovingSound();
        }

    }

    private void MovingSound()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.X))
        {
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.X))
        {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthPackCollision(collision);
        MunitionPackCollision(collision);
        MinePackCollision(collision);
        ShieldBonusCollision(collision);
        ThunderBoltBonusCollision(collision);
        if (collision.gameObject.CompareTag("UnlimitedAmmunition"))
        {
            Destroy(collision.gameObject);
            isUnlimitedMunition = true;
        }
    }

    private void ThunderBoltBonusCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("ThunderBolt"))
        {
            Destroy(collision.gameObject);
            isThunderBoltBonus = true;
            speedTranslation = 50;
            speedParticle.Play();
        }
    }

    private void ShieldBonusCollision(Collision collision)
    {
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
    }

    private void MinePackCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("MinePack"))
        {
            Destroy(collision.gameObject);
            numberMine += 1;
        }
    }

    private void MunitionPackCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("MunitionPack"))
        {
            Destroy(collision.gameObject);
            numberMunition += 1;
        }
    }

    private void HealthPackCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealthPack"))
        {

            Destroy(collision.gameObject);
            healthBar.GetComponent<Scrollbar>().size = 1;
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

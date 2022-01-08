using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementSpeed;
    [Header("Speed")]
    [SerializeField] float normalMovementSpeed;
    [SerializeField] float boostMovementSpeed;
    [SerializeField] float secondsWithoutBoost = 0.15f;

    [Header("Impulse")]
    [SerializeField] float impulseMovementSpeed;
    [SerializeField] float rollbackStepAfterImpulse;

    [Header("Energy")]
    [SerializeField] int maxEnergy;
    [SerializeField] float rollbackStepEnergy = 0.03f, energyRestoreStep = 0.02f;

    int direction = 0, curHP;
    float curEnergy;

    [Header("Health")]
    public int maxHP;
    public int damage;

    [Header("UI")]
    [SerializeField] RectTransform hpBar;
    [SerializeField] RectTransform energyBar;

    SystemTimers timers;
    Main main;
    [SerializeField] PostProcessing postProcessing;
    SoundEffector sound;

    [Header("Items")]
    [SerializeField] SpriteRenderer shieldImg;
    
    [Header("Shooting")]
    [SerializeField] float shootingReloadTime;
    [SerializeField] Transform bulletSpawner;
    [SerializeField] float bulletSpeed;
    [SerializeField] Bullet bullet;

    [Header("Upgrades")]
    [SerializeField] Explosion explosion;

    [Header("Explode")]
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionDuration;

    [Header("Particles")]
    [SerializeField] GameObject deathParticle;
    [SerializeField] GameObject getDamageParticle;

    PlayerRocketTail rocketTailObject;

    private void Start()
    {
        curHP = PlayerPrefs.GetInt("HP");
        rocketTailObject = FindObjectOfType<PlayerRocketTail>();
        hpBar.localScale = new Vector2(1f / maxHP * curHP, 1);
        curEnergy = maxEnergy;
        movementSpeed = normalMovementSpeed;
        timers = SystemTimers.Instance;
        main = Main.Instance;
        sound = SoundEffector.Instance;

        CheckUpgrades();

        explosion.damage = damage;
        explosion.duration = explosionDuration;
        explosion.radius = explosionRadius;
    }

    void CheckUpgrades()
    {
        if (PlayerPrefs.GetInt("ShootingWhenRotate") == 1)
            ShootingWhenRotate();
        if (PlayerPrefs.GetInt("BoostSpeed") == 1)
            BoostSpeed();
        if (PlayerPrefs.GetInt("Explode") == 1)
            Explode();
        if (PlayerPrefs.GetInt("RocketTail") == 1)
            RocketTail();
    }

    float timer = 0;

    [HideInInspector] public bool energy = true;
    private void Update()
    {
        if (canMovement)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timer = 0;
                if (shootingWhenRotate)
                    ToShoot();
                Rotate();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                timer += Time.deltaTime;
                if (timer > secondsWithoutBoost)
                {
                    if (energy)
                    {
                        Boost();
                        if (rocketTail)
                            rocketTailObject.OpenTail();
                        postProcessing.Boost();
                    }
                    MinusEnergy();
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                energy = true;
                EndBoost();
            }
        }
        RestoreEnergy();
    }

    private void FixedUpdate()
    {
        if(canMovement) 
            Movement();
    }

    //Movement

    void Movement()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.fixedDeltaTime);
    }

    void Rotate()
    {
        direction++;
        if (direction == 4)
            direction = 0;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * direction);
    }

    bool canMovement = true;
    public void OffMovement()
    {
        EndBoost();
        canMovement = false;
    }

    public void OnMovement()
    {
        canMovement = true;
    }

    //Boost

    void Boost()
    {
        if (curEnergy > rollbackStepEnergy)
        {
            movementSpeed = boostMovementSpeed;
            boosted = true;
        }
        else
            energy = false;
    }

    void MinusEnergy()
    {
        if (boosted && curEnergy > rollbackStepEnergy)
        {
            curEnergy -= rollbackStepEnergy;
            energyBar.localScale = new Vector2(1f / maxEnergy * curEnergy, 1);
        }
        else if (boosted && curEnergy <= rollbackStepEnergy)
        {
            EndBoost();
        }
    }

    void RestoreEnergy()
    {
        if (curEnergy < maxEnergy && !boosted)
        {
            curEnergy += energyRestoreStep;
            energyBar.localScale = new Vector2(1f / maxEnergy * curEnergy, 1);
        }
    }

    void EndBoost()
    {
        if (rocketTail)
            rocketTailObject.CloseTail();
        postProcessing.EndBoost();
        movementSpeed = normalMovementSpeed;
        boosted = false;
    }

    //Impulse

    public void SetImpulse()
    {
        StartCoroutine(Impulse());
    }

    IEnumerator Impulse()
    {
        movementSpeed = impulseMovementSpeed;
        while (movementSpeed > normalMovementSpeed)
        {
            movementSpeed -= rollbackStepAfterImpulse;
            yield return new WaitForEndOfFrame();
        }
        movementSpeed = normalMovementSpeed;
    }

    //Health

    public void GetDamage(int damage)
    {
        if (!shield)
        {
            curHP -= damage;
            if(explode)
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y, -2);
                Instantiate(explosion.gameObject, position, Quaternion.identity);
            }
            if(curHP > 0)
            {
                PlayerPrefs.SetInt("HP", curHP);
                hpBar.localScale = new Vector2(1f / maxHP * curHP, 1);
                Instantiate(getDamageParticle, transform.position, Quaternion.identity);
                sound.PlayGetDamage();
            }
            else if (curHP <= 0)
            {
                curHP = 0;
                hpBar.localScale = new Vector2(1f / maxHP * curHP, 1);
                Death();
            }
        }
    }

    public void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        main.Death();
        sound.PlayLose();
        Destroy(gameObject);
    }

    //Items Effects

    [HideInInspector] public bool boosted = false, shield = false, infinityEnergy = false;

    public void Healing(int heal)
    {
        curHP += heal;
        if (curHP > maxHP)
            curHP = maxHP;
        PlayerPrefs.SetInt("HP", curHP);
        hpBar.localScale = new Vector2(1f / maxHP * curHP, 1);
    }


    public void StartInfinityEnergy(float secondsInfinityEnergy, Sprite sprite, Color color)
    {
        StartCoroutine(SetInfinityEnergy(secondsInfinityEnergy, sprite, color));
    }

    IEnumerator SetInfinityEnergy(float secondsInfinityEnergy, Sprite sprite, Color color)
    {
        float rollbackStepEnergy = this.rollbackStepEnergy;
        this.rollbackStepEnergy = 0;
        infinityEnergy = true;
        timers.CreateTimer(sprite, color, secondsInfinityEnergy);
        yield return new WaitForSeconds(secondsInfinityEnergy);
        this.rollbackStepEnergy = rollbackStepEnergy;
        infinityEnergy = false;
    }


    public void StartShield(float secondsShield, Sprite sprite, Color color)
    {
        StartCoroutine(SetShield(secondsShield, sprite, color));
    }

    IEnumerator SetShield(float secondsShield, Sprite sprite, Color color)
    {
        shield = true;
        shieldImg.color = new Color(shieldImg.color.r, shieldImg.color.g, shieldImg.color.b, 1);
        timers.CreateTimer(sprite, color, secondsShield);
        yield return new WaitForSeconds(secondsShield - 2);
        StartCoroutine(Flashing(4, 0.25f, shieldImg));
        yield return new WaitForSeconds(2);
        shield = false;
        shieldImg.color = new Color(shieldImg.color.r, shieldImg.color.g, shieldImg.color.b, 0);
    }

    IEnumerator Flashing(int numberFlashing, float timeFlash, SpriteRenderer sprite)
    {
        for (int i = 0; i < numberFlashing; i++)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            yield return new WaitForSeconds(timeFlash);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            yield return new WaitForSeconds(timeFlash);
        }
    }


    [HideInInspector] public bool shooting = false;
    public void StartShooting(float secondsShooting, Sprite sprite, Color color)
    {
        bullet.speed = bulletSpeed;
        bullet.damage = damage;
        StartCoroutine(Shooting(secondsShooting, sprite, color));
    }

    IEnumerator Shooting(float secondsShooting, Sprite sprite, Color color)
    {
        timers.CreateTimer(sprite, color, secondsShooting);
        shooting = true;
        for (int i = 0; i < secondsShooting / shootingReloadTime; i++)
        {
            ToShoot();
            yield return new WaitForSeconds(shootingReloadTime);
        }
        shooting = false;
    }

    void ToShoot()
    {
        Instantiate(bullet.gameObject, bulletSpawner.position, transform.rotation);
    }

    //Upgrades

    bool shootingWhenRotate = false, boostSpeed = false, explode = false, rocketTail = false;

    public void ShootingWhenRotate()
    {
        shootingWhenRotate = true;
        PlayerPrefs.SetInt("ShootingWhenRotate", 1);
    }

    public void BoostSpeed()
    {
        rollbackStepEnergy = 0.035f;
        boostMovementSpeed++;
        boostSpeed = true;
        PlayerPrefs.SetInt("BoostSpeed", 1);
    }

    public void Explode()
    {
        explode = true;
        PlayerPrefs.SetInt("Explode", 1);
    }

    public void RocketTail()
    {
        rocketTail = true;
        PlayerPrefs.SetInt("RocketTail", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float movementSpeed, rotationSpeed;

    [SerializeField] public int damage , maxHP;

    [SerializeField] protected GameObject deathParticle;
 
    protected int curHP;

    protected Transform target;
    protected MainEnemySpawner spawner;
    protected Player player;
    protected SoundEffector sound;

    protected virtual void Start()
    {
        curHP = maxHP;
        spawner = MainEnemySpawner.Instance;
        spawner.enemies.Add(GetComponent<Enemy>());
        target = FindObjectOfType<Player>().transform;
        player = target.GetComponent<Player>();
        sound = SoundEffector.Instance;
    }

    protected virtual void Update()
    {
        Rotation();
    }

    protected virtual void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.fixedDeltaTime);
    }

    protected virtual void Rotation()
    {
        float directionTowardsPlayer = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * (180 / Mathf.PI) - 90;
        Quaternion futureAngle = Quaternion.Euler(new Vector3(0, 0, directionTowardsPlayer));
        transform.rotation = Quaternion.Lerp(transform.rotation, futureAngle, Time.deltaTime * rotationSpeed);
    }

    public void GetDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
            Death();
    }

    public virtual void ToDamage()
    {
        player.SetImpulse();
        player.GetDamage(damage);
    }

    protected virtual void Death()
    {
        spawner.EnemyDeath(this);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        sound.PlayEnemyDeath();
        Destroy(gameObject);
    }
}

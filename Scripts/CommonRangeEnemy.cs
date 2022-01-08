using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonRangeEnemy : Enemy
{
    [Space]

    [SerializeField] float maxDistanceToPlayer;
    [SerializeField] int reloadTime;
    int curReloadTime;
    [SerializeField] Text reloadTimeText;
    [SerializeField] Bullet bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletSpawner;
    [SerializeField] Transform canvas;

    bool inPlayerKillZone;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shooting());
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) > maxDistanceToPlayer)
            Movement();
        canvas.rotation = Quaternion.Euler(Vector3.zero);
    }

    protected override void Death()
    {
        base.Death();
        Destroy(canvas.gameObject);
    }

    IEnumerator Shooting()
    {
        curReloadTime = reloadTime;
        UpdateReloadText();
        while (curReloadTime != 0)
        {
            yield return new WaitForSeconds(1);
            curReloadTime--;
            UpdateReloadText();
        }
        if(inPlayerKillZone)
            ToShoot();
        StartCoroutine(Shooting());
    }

    private void OnBecameVisible()
    {
        inPlayerKillZone = true;
    }

    private void OnBecameInvisible()
    {
        inPlayerKillZone = false;
    }

    void ToShoot()
    {
        sound.PlayEnemyShot();
        Instantiate(bullet.gameObject, bulletSpawner.position, transform.rotation);
    }

    private void UpdateReloadText()
    {
        reloadTimeText.text = curReloadTime.ToString();
    }
}

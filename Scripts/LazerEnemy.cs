using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerEnemy : Enemy
{
    [Space]

    [SerializeField] float maxDistanceToPlayer;
    [SerializeField] int reloadTime;
    int curReloadTime;
    [SerializeField] Text reloadTimeText;
    [SerializeField] Lazer lazer;
    [SerializeField] float lazerDuration;
    [SerializeField] Transform lazerSpawner;
    [SerializeField] Transform canvas;

    bool inPlayerKillZone, canMovement = true, canRotation = true;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Lazering());
        lazer.damage = damage;
        lazer.duration = lazerDuration;
        
    }

    protected override void Update()
    {
        if(canRotation)
            Rotation();
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) > maxDistanceToPlayer)
        {
            if(canMovement)
                Movement();
        }
        canvas.rotation = Quaternion.Euler(Vector3.zero);
    }

    protected override void Death()
    {
        base.Death();
        Destroy(canvas.gameObject);
    }

    IEnumerator Lazering()
    {
        curReloadTime = reloadTime;
        UpdateReloadText();
        while (curReloadTime != 0)
        {
            yield return new WaitForSeconds(1);
            curReloadTime--;
            UpdateReloadText();
        }
        if (inPlayerKillZone) {
            SpawnLazer();
            OffReloadText();
            OffMove();
            yield return new WaitForSeconds(lazerDuration);
            OnMove();
            OnReloadText();
        }
        
        StartCoroutine(Lazering());
    }

    private void OnBecameVisible()
    {
        inPlayerKillZone = true;
    }

    private void OnBecameInvisible()
    {
        inPlayerKillZone = false;
    }

    void OnMove()
    {
        canMovement = true;
        canRotation = true;
    }

    void OffMove()
    {
        canMovement = false;
        canRotation = false;
    }

    void SpawnLazer()
    {
        Vector3 position = new Vector3(lazerSpawner.position.x, lazerSpawner.position.y, -2);
        Quaternion rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
        lazer.parent = gameObject;
        sound.PlayEnemyShot();
        Instantiate(lazer.gameObject, position, rotation);
    }

    private void UpdateReloadText()
    {
        reloadTimeText.text = curReloadTime.ToString();
    }

    void OffReloadText()
    {
        reloadTimeText.gameObject.SetActive(false);
    }

    void OnReloadText()
    {
        reloadTimeText.gameObject.SetActive(true);
    }
}

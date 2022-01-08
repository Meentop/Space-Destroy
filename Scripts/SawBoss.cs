using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBoss : MonoBehaviour
{
    Player player;

    [SerializeField] int maxHP = 5, curHP, countHeartInPhase = 5;
    int phase = 1;

    [SerializeField] SawBossHeart heart;

    [SerializeField] List<Saw> saws;

    [SerializeField] Vector2 spawnBorder1, spawnBorder2;

    [SerializeField] Transform spawnHeartsPlace, bossPlace1, bossPlace2;

    [SerializeField] float speed;
    [SerializeField] GameObject health, deathParticle;

    public static SawBoss Instance;

    UpgradePanel upgradePanel;

    bool nearToPlayer = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        curHP = maxHP;
        upgradePanel = UpgradePanel.Instance;
        StartCoroutine(StartFight());
    }

    private void Update()
    {
        BossMovement();
        if (curHP == 0 && phase < 3)
            EndPhase();
        if (curHP == 0 && phase == 3)
            EndFight();
    }

    void BossMovement()
    {
        if (nearToPlayer)
            transform.position = Vector3.MoveTowards(transform.position, bossPlace1.position, speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, bossPlace2.position, speed * Time.deltaTime);
    }

    void EndPhase()
    {
        curHP = maxHP;
        foreach (Saw saw in saws)
        {
            saw.Off();
        }
        phase++;
        StartCoroutine(StartFight());
    }

    void EndFight()
    {
        foreach (Saw saw in saws)
        {
            Destroy(saw.gameObject);
        }
        health.SetActive(true);
        PlayerPrefs.SetInt("Stage", 1);
        upgradePanel.ShowUpgades();
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator StartFight()
    {
        player.OffMovement();
        nearToPlayer = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnHearts());
        yield return new WaitForSeconds(1 * countHeartInPhase);
        nearToPlayer = false;
        yield return new WaitForSeconds(1);
        player.OnMovement();
        EnableSaw();
    }

    IEnumerator SpawnHearts()
    {
        heart.spawnBorder1 = this.spawnBorder1;
        heart.spawnBorder2 = this.spawnBorder2;
        for (int i = 0; i < countHeartInPhase; i++)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(heart.gameObject, spawnHeartsPlace.position, Quaternion.identity);
        }
    }

    void EnableSaw()
    {
        for (int i = 0; i < phase; i++)
        {
            saws[i].Enable();
        }
    }

    public void GetDamage()
    {
        curHP--;
    }
}

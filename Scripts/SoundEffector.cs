using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    AudioSource audio;

    public static SoundEffector Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    [SerializeField] List<AudioClip> getDamageClip;

    public void PlayGetDamage()
    {
        audio.PlayOneShot(getDamageClip[Random.Range(0, getDamageClip.Count)]);
    }

    [SerializeField] AudioClip lose;

    public void PlayLose()
    {
        audio.PlayOneShot(lose);
    }

    [SerializeField] List<AudioClip> enemyDeathClip;

    public void PlayEnemyDeath()
    {
        audio.PlayOneShot(enemyDeathClip[Random.Range(0, enemyDeathClip.Count)]);
    }

    [SerializeField] List<AudioClip> explosionClip;

    public void PlayExplosion()
    {
        audio.PlayOneShot(explosionClip[Random.Range(0, explosionClip.Count)]);
    }

    [SerializeField] List<AudioClip> enemyShotClip;

    public void PlayEnemyShot()
    {
        audio.PlayOneShot(enemyShotClip[Random.Range(0, enemyShotClip.Count)]);
    }

    [SerializeField] List<AudioClip> itemClip;

    public void PlayItem()
    {
        audio.PlayOneShot(itemClip[Random.Range(0, itemClip.Count)]);
    }

    [SerializeField] AudioClip upgradeClip;

    public void PlayUpgrade()
    {
        audio.PlayOneShot(upgradeClip);
    }
}

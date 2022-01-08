using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBossHeart : MonoBehaviour
{
    SawBoss boss;

    public Vector2 spawnBorder1, spawnBorder2;

    [SerializeField] float speed;

    Vector3 place;

    bool inPosition = false;

    [SerializeField] GameObject destroyParticle;

    SoundEffector sound;

    private void Start()
    {
        boss = SawBoss.Instance;
        sound = SoundEffector.Instance;
        place = new Vector3(Random.Range(spawnBorder1.x, spawnBorder2.x), Random.Range(spawnBorder1.y, spawnBorder2.y), transform.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, place, speed * Time.deltaTime);
        if (transform.position == place)
            inPosition = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && inPosition)
        {
            boss.GetDamage();
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            sound.PlayEnemyDeath();
            Destroy(gameObject);
        }
    }
}

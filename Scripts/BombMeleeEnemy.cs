using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombMeleeEnemy : Enemy
{
    [Space]

    [SerializeField] int timeToExplosion;
    int curTimeToExplosion;

    [SerializeField] Transform canvas;
    [SerializeField] Text timerText;
    [SerializeField] Explosion explosion;
    [SerializeField] float explosionRadius, explosionDuration;

    SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Timer());
        explosion.duration = explosionDuration;
        explosion.radius = explosionRadius;
        explosion.damage = damage;

        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Flashing());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        canvas.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator Timer()
    {
        curTimeToExplosion = timeToExplosion;
        UpdateTimerText();
        while (curTimeToExplosion != 0)
        {
            yield return new WaitForSeconds(1);
            curTimeToExplosion--;
            UpdateTimerText();
        }
        Death();
    }

    private void UpdateTimerText()
    {
        timerText.text = curTimeToExplosion.ToString();
    }

    public override void ToDamage()
    {
        Death();
    }

    protected override void Death()
    {
        spawner.EnemyDeath(this);
        Destroy(gameObject);
        Destroy(canvas.gameObject);
        Explosion();
    }

    void Explosion()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        Instantiate(explosion.gameObject, position, Quaternion.identity);
    }

    IEnumerator Flashing()
    {
        sprite.color = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Flashing());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketTail : PlayersSword
{
    [SerializeField] float maxScale = 3;
    [SerializeField] GameObject particle;

    public void OpenTail()
    {
        StopAllCoroutines();
        StartCoroutine(OpenTailCoroutine());
        particle.SetActive(true);
    }

    public void CloseTail()
    {
        StopAllCoroutines();
        StartCoroutine(CloseTailCoroutine());
        particle.SetActive(false);
    }

    IEnumerator OpenTailCoroutine()
    {
        while(transform.localScale.x < maxScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CloseTailCoroutine()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f, transform.localScale.z);
            yield return new WaitForEndOfFrame();
        }
    }
}

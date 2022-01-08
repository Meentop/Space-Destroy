using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    [HideInInspector] public float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float time = 0.2f;
    [Range(0f, 2f)]
    public float distance = 0.1f;
    [Range(0f, 0.1f)]
    public float delayBetweenShakes = 0f;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (delayBetweenShakes > time)
            delayBetweenShakes = time;
    }

    public void Begin()
    {
        _startPos = transform.position;
        StopAllCoroutines();
        StartCoroutine(Shakeing());
    }

    private IEnumerator Shakeing()
    {
        _timer = 0f;

        while (_timer < time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * distance);

            transform.position = _randomPos;

            if (delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        transform.position = _startPos;
    }

}

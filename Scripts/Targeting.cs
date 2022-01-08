using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] protected Transform target;

    [SerializeField] float lerpSpeed;

    protected virtual void FixedUpdate()
    {
        Vector3 lerp = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
    }
}

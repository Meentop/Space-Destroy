using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemTimers : MonoBehaviour
{
    [SerializeField] GameObject timer;

    public List<Transform> timers;

    public static SystemTimers Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateTimer(Sprite sprite, Color color, float timeStart)
    {
        timer.GetComponent<Timer>().StartTimer(sprite, color, timeStart);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - (timers.Count * 0.5f), transform.position.z);
        Instantiate(timer, pos, Quaternion.identity, transform);
    }

    public void DestroyTimer(Transform timer)
    {
        int timerCount = timers.IndexOf(timer);
        timers.Remove(timer);
        for(int i = timerCount; i < timers.Count; i++)
            timers[i].position = new Vector3(timers[i].position.x, timers[i].position.y + 0.5f, timers[i].position.z);
        Destroy(timer.gameObject);
    }
}

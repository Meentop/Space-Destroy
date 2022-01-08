using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] float timeStart;

    [SerializeField] Text timerText;
    [SerializeField] Image image;

    SystemTimers timers;

    void Start()
    {
        timers = SystemTimers.Instance;
        timers.timers.Add(transform);
        timerText.text = timeStart.ToString();
    }

    private void Update()
    {
        timeStart -= Time.deltaTime;
        if (Mathf.Round(timeStart) == -1)
            timers.DestroyTimer(transform);
        timerText.text = Mathf.Round(timeStart).ToString();
    }

    public void StartTimer(Sprite sprite, Color color, float timeStart)
    {
        image.sprite = sprite;
        image.color = color;
        timerText.color = color;
        this.timeStart = timeStart;
    }
}

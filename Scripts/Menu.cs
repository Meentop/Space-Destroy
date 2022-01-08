using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Shake shake;

    [SerializeField] float riseShakeDistance = 0.01f;

    [SerializeField] int maxHP = 10;

    private void Start()
    {
        PlayerPrefs.SetInt("ShootingWhenRotate", 0);
        PlayerPrefs.SetInt("BoostSpeed", 0);
        PlayerPrefs.SetInt("Explode", 0);
        PlayerPrefs.SetInt("RocketTail", 0);
        PlayerPrefs.SetInt("HP", maxHP);
        PlayerPrefs.SetInt("Stage", 0);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
            SceneManager.LoadScene(1);
        if(Input.GetKeyDown(KeyCode.Space))
            shake.Begin();
        if(Input.GetKey(KeyCode.Space))
            shake.distance += riseShakeDistance;

        if(shake._timer >= 2f)
            Application.Quit();
    }
}

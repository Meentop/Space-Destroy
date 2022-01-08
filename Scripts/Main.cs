using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] float timeAfterDeath = 2f;
    [SerializeField] Text stageText;
    [SerializeField] List<string> stages;

    public static Main Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StageText());
    }

    IEnumerator StageText()
    {
        stageText.gameObject.SetActive(true);
        stageText.text = stages[PlayerPrefs.GetInt("Stage")];
        yield return new WaitForSeconds(2);
        while(stageText.color.a > 0)
        {
            stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, stageText.color.a - 0.016f);
            yield return new WaitForEndOfFrame();
        }
        stageText.gameObject.SetActive(false);
    }

    public void Death()
    {
        StartCoroutine(AfterDeath());
    }

    IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(timeAfterDeath);
        SceneManager.LoadScene(0);
    }
}

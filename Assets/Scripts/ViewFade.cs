using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewFade : MonoBehaviour
{
    public Image fadeImage;
    public GameObject canvas;
    public float FadeTime = 2f;

    private void Start()
    {
        FadeIn();
    }

    public void FadeOut(string NextLevel)
    {
        StartCoroutine(Transition(0f, 1f, FadeTime, true, NextLevel));
    }

    public void FadeIn()
    {
        StartCoroutine(Transition(1f, 0f, FadeTime, false, string.Empty));
    }

    private IEnumerator Transition(float AlphaStart, float AlphaFinish, float FadeTime, bool keepFaded, string NextLevel)
    {
        float delaystart = Time.time;
        float delayprogress = 0f;

        Color startColor = new Color(0, 0, 0, AlphaStart);
        Color finishColor = new Color(0, 0, 0, AlphaFinish);

        fadeImage.color = startColor;
        canvas.SetActive(true);

        do
        {
            delayprogress = (Time.time - delaystart) / FadeTime;
            fadeImage.color = Color.Lerp(startColor, finishColor, delayprogress);
            yield return null;
        } while (delayprogress < 1);

        canvas.SetActive(keepFaded);

        if (NextLevel != string.Empty)
        {
            SceneManager.LoadScene(NextLevel);
        }

    }
}

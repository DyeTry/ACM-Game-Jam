using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public float chipSpeed = 2f;
    private float lerpTimer;

    public Image frontLoadingBar;
    public Image backLoadingBar;
    public Image frame;

    void Start()
    {
        frontLoadingBar.enabled = false;
        backLoadingBar.enabled = false;
        frame.enabled = false;
    }

    public void ShowLoadingBar(float duration) { StartCoroutine(LoadingBarCoroutine(duration)); }

    IEnumerator LoadingBarCoroutine(float duration)
    {
        frontLoadingBar.enabled = true;
        backLoadingBar.enabled = true;
        frame.enabled = true;

        float timer = 0f;

        while (timer < duration)
        {
            lerpTimer = Mathf.Clamp01(timer / duration);
            frontLoadingBar.fillAmount = Mathf.Lerp(0f, 1f, lerpTimer);
            timer += Time.deltaTime;
            yield return null;
        }

        frontLoadingBar.fillAmount = 1f;

        frontLoadingBar.enabled = false;
        backLoadingBar.enabled = false;
        frame.enabled = false;
    }
}
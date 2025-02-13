using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFX : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private GameObject[] letters;
    private float timeActive;

    [SerializeField] private Slider[] sliders;

    private void Start()
    {
        UIManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        TitleSinWave();
    }

    private void TitleSinWave()
    {
        timeActive += Time.deltaTime;
        for (int i = 0; i < letters.Length; i++)
        {
            GameObject letter = letters[i];
            letter.transform.localPosition = new Vector3(letter.transform.localPosition.x, Mathf.Sin(timeActive * 1.5f - i) * 50);
        }
    }

    public void StartSlide(int index)
    {
        StartCoroutine(SlideEffect(index));
    }

    private IEnumerator SlideEffect(int index)
    {
        yield return Slide();
        UIManager.SwapUI(index);
        yield return Slide();
        foreach (Slider slider in sliders)
            slider.ResetPosition();
    }

    private IEnumerator Slide()
    {
        foreach (Slider slider in sliders)
        {
            StartCoroutine(slider.Slide());
            yield return new WaitForSeconds(0.125f);
        }
        yield return new WaitUntil(() => Array.TrueForAll(sliders, slider => slider.Finished));
    }
}

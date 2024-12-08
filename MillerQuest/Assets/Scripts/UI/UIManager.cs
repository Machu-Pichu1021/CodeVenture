using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIs;

    private void Start()
    {
        SwapUI(0);
    }

    public void SwapUI(int index)
    {
        Array.ForEach(UIs, ui => ui.SetActive(false));
        UIs[index].SetActive(true);
    }

    public void NextUI()
    {
        int activeIndex = Array.IndexOf(UIs, Array.Find(UIs, ui => ui.activeInHierarchy));
        if (activeIndex != UIs.Length - 1)
        {
            Array.ForEach(UIs, ui => ui.SetActive(false));
            UIs[activeIndex + 1].SetActive(true);
        }
    }

    public void PreviousUI()
    {
        int activeIndex = Array.IndexOf(UIs, Array.Find(UIs, ui => ui.activeInHierarchy));
        if (activeIndex != 0)
        {
            Array.ForEach(UIs, ui => ui.SetActive(false));
            UIs[activeIndex - 1].SetActive(true);
        }
    }
}

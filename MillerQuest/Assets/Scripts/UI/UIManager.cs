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
}

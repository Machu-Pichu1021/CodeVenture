using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutputHandler : MonoBehaviour
{
    public static OutputHandler instance;

    [SerializeField] private TMP_Text outputText;

    private string output;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddOutput(string additional)
    {
        output += additional;
        UpdateOutput();
    }

    private void UpdateOutput()
    {
        outputText.text = output;
    }
}

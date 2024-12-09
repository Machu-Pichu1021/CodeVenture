using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorLogger : MonoBehaviour
{
    public static ErrorLogger instance;
    [SerializeField] private AudioClip errorSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void LogError(string errorMessage, int lineNumber)
    {
        AudioManager.instance.PlaySFX(errorSFX);
        OutputHandler.instance.ClearOutput();
        OutputHandler.instance.ChangeTextColor(Color.red);
        OutputHandler.instance.AddOutput(errorMessage + " Line Number: " + lineNumber);
    }
}

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

    public void LogError(string errorMessage)
    {
        AudioManager.instance.PlaySFX(errorSFX);
        OutputHandler.instance.ClearOutput();
        OutputHandler.instance.ChangeTextColor(Color.red);
        OutputHandler.instance.AddOutput(errorMessage + " Line Number: " + (Executer.instance.LineNumber + 1));
        Executer.instance.HaltControl();
        VariableTracker.instance.ClearVariables();
    }

    public bool IsLiteral(string v)
    {
        return float.TryParse(v, out _) || bool.TryParse(v, out _) || (v.Length > 1 && v[0] == '"' && v[v.Length - 1] == '"');
    }
}

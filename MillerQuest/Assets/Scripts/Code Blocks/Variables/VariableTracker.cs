using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTracker : MonoBehaviour
{
    public static VariableTracker instance;
    private Dictionary<string, object> variables = new Dictionary<string, object>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    public void AddVariable(string varName, object value)
    {
        if (!variables.ContainsKey(varName))
            variables.Add(varName, value);
        else
            ErrorLogger.instance.LogError("Error Code 4: Variable " + varName + " has already been assigned to.");
    }

    public bool TryGetValue(string varName, out object value)
    {
        value = null;
        if (variables.TryGetValue(varName, out object v))
        {
            value = v;
            return true;
        }
        else
            return false;

    }

    public void ClearVariables()
    {
        variables = new Dictionary<string, object>();
    }
}

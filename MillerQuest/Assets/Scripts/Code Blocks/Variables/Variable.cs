using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable : CodeBlock
{
    [HideInInspector] public string variableName;
    [HideInInspector] public string value;

    public void UpdateName(string name)
    {
        variableName = name;
    }

    public void UpdateValue(string value)
    {
        this.value = value;
    }

    public bool InvalidVariableName()
    {
        return variableName == "" || float.TryParse(variableName, out _) || bool.TryParse(variableName, out _);
    }
}

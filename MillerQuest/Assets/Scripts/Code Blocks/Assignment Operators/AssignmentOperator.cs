using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AssignmentOperator : CodeBlock
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
}

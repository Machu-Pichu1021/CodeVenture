using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringAssignment : Variable
{
    public override void Execute()
    {
        if (InvalidVariableName())
            ErrorLogger.instance.LogError("Error Code 2: Invalid Variable Name.");
        else if (VariableTracker.instance.TryGetValue(value, out object v))
        {
            if (v is string)
                VariableTracker.instance.AddVariable(variableName, v);
        }
        else if (value == "")
            ErrorLogger.instance.LogError("Error Code 5: Missing Argument.");
        else if (value == "\"")
            ErrorLogger.instance.LogError("Error Code 6: Unclosed String.");
        else if (value[0] != '"' || value[value.Length - 1] != '"')
            ErrorLogger.instance.LogError("Error Code 3: Invalid Variable Assignment.");
        else
            VariableTracker.instance.AddVariable(variableName, value[1..(value.Length - 1)]);
    }
}

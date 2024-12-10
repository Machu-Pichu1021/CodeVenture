using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAssignment : Variable
{
    public override void Execute()
    {
        if (InvalidVariableName())
            ErrorLogger.instance.LogError("Error Code 2: Invalid Variable Name.");
        else if (VariableTracker.instance.TryGetValue(value, out object v))
        {
            if (v is bool)
                VariableTracker.instance.AddVariable(variableName, v);
        }
        else if (value == "")
            ErrorLogger.instance.LogError("Error Code 5: Missing Argument.");
        else if (bool.TryParse(value, out bool val))
            VariableTracker.instance.AddVariable(variableName, val);
        else
            ErrorLogger.instance.LogError("Error Code 3: Invalid Variable Assignment.");
    }
}

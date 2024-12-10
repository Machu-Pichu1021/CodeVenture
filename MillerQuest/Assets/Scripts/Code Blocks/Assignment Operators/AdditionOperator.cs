using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionOperator : AssignmentOperator
{
    public override void Execute()
    {
        if (ErrorLogger.instance.IsLiteral(variableName))
            ErrorLogger.instance.LogError("Error Code 7: Left hand side of an operator must be a variable.");
        else if (VariableTracker.instance.TryGetValue(variableName, out object v))
        {
            if (VariableTracker.instance.TryGetValue(value, out object v2))
            {
                if (v is int && v2 is int)
                    VariableTracker.instance.UpdateVariable(variableName, (int)v + (int)v2);
                else if (v is float && v2 is int)
                    VariableTracker.instance.UpdateVariable(variableName, (float)v + (int)v2);
                else if (v is float && v2 is float)
                    VariableTracker.instance.UpdateVariable(variableName, (float)v + (float)v2);
                else if (v is string)
                    VariableTracker.instance.UpdateVariable(variableName, v.ToString() + v2.ToString());
                else
                    ErrorLogger.instance.LogError("Error Code 8: Operator '+' cannot be used between these types.");

            }
            else if (ErrorLogger.instance.IsLiteral(value))
            {
                if (v is int && int.TryParse(value, out int r1))
                    VariableTracker.instance.UpdateVariable(variableName, (int)v + r1);
                else if (v is float && float.TryParse(value, out float r2))
                    VariableTracker.instance.UpdateVariable(variableName, (float)v + r2);
                else if (v is string)
                    VariableTracker.instance.UpdateVariable(variableName, v.ToString() + value);
                else
                    ErrorLogger.instance.LogError("Error Code 8: Operator '+' cannot be used between these types.");
            }
            else
                ErrorLogger.instance.LogError("Error Code 1: Cannot resolve symbol '" + value + "'.");
        }
        else
            ErrorLogger.instance.LogError("Error Code 1: Cannot resolve symbol '" + variableName + "'.");
    }
}

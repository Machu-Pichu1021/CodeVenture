using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLine : CodeBlock
{
    private string argument;
    
    public override void Execute()
    {
        if (argument == "")
            ErrorLogger.instance.LogError("Error Code 5: Missing Argument.");
        else if ((argument.Length - argument.Replace("\"", "").Length) % 2 == 1)
            ErrorLogger.instance.LogError("Error Code 6: Unclosed String.");
        else if (argument[0] == '"' && argument[argument.Length - 1] == '"')
        {
            string output = argument[1..(argument.Length - 1)];
            OutputHandler.instance.AddOutput(output + "\n");
        }
        else if (VariableTracker.instance.TryGetValue(argument, out object value))
        {
            string output = value.ToString();
            OutputHandler.instance.AddOutput(output + "\n");
        }
        else
            ErrorLogger.instance.LogError("Error Code 1: Cannot Resolve Symbol '" + argument + "'.");

    }

    public void UpdateArgument(string input)
    {
        argument = input;
    }

    private void OnMouseDown()
    {
        UpdateArgument("");
    }
}

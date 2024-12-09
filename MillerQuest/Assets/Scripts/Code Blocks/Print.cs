using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Print : CodeBlock
{
    private string argument;

    public override void Execute()
    {
        //Add variable checking later
        if (argument[0] == '"' && argument[argument.Length - 1] == '"')
        {
            string output = argument[1..(argument.Length - 1)];
            OutputHandler.instance.AddOutput(output + "\n");
        }
        else
        {
            ErrorLogger.instance.LogError("Error Code 1: Cannot Resolve Symbol '" + argument + "'.");
        }
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

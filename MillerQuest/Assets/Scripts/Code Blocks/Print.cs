using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Print : CodeBlock
{
    private string argument;

    public override void Execute()
    {
        OutputHandler.instance.AddOutput(argument);
    }

    public void UpdateArgument(string input)
    {
        argument = input;
    }
}

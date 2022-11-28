using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNodeExample : Node
{
    //private variables
    private int example;
    private string stringexample;

    //constructor to set the private variables
    public CustomNodeExample(int Example, string stringexampole)
    {
        example = Example;
        stringexample = stringexampole;
    }
    //main method
    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
    
    //other custom methods for fullfiling the Evaluate
}

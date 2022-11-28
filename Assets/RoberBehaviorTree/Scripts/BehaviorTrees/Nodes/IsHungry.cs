using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHungry : Node
{
    //private variables
    private GameObject CreatureBody;
    private float MinHunger;

    //constructor to set the private variables
    public IsHungry(GameObject creaturebody, float minHunger)
    {
        this.CreatureBody = creaturebody;
        this.MinHunger = minHunger;
    }
    //main method
    public override NodeState Evaluate()
    {
        if(CreatureBody.GetComponent<DumbCreature>().hunger < MinHunger)
        {
            return NodeState.SUCCES;
        }
        //aici un else if cu get component scriptu altei creaturi
        else
        {
            return NodeState.FAILURE;
        }
    }
    
    //other custom methods for fullfiling the Evaluate
}

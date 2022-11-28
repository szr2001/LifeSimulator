using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Node
{
    private IsFoodClose Ifc;
    private GameObject CreatureBody;
    private GameObject FoodTarget;
    public Eat(IsFoodClose ifc, GameObject creatureBody)
    {
        this.Ifc = ifc;
        this.CreatureBody = creatureBody;
    }
    //main method
    public override NodeState Evaluate()
    {
        FoodTarget = Ifc.GetPlantTarget();
        Vector3 Distance = CreatureBody.transform.position - FoodTarget.transform.position;
        if (Distance.magnitude <= 2f)
        {
            float Nhun = FoodTarget.GetComponent<FoodScript>().Eat(CreatureBody.GetComponent<DumbCreature>().hunger);
            CreatureBody.GetComponent<DumbCreature>().hunger = Nhun;
            CreatureBody.GetComponent<DumbCreature>().CreatureBehavior = Creature.Behavior.Eating;
            return NodeState.SUCCES;
        }
        return NodeState.RUNNING;
    }
    
    //other custom methods for fullfiling the Evaluate
}

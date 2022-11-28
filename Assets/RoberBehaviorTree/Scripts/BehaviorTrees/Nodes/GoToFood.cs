using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToFood : Node
{

    private GameObject CreatureBody;
    private GameObject PlantTarget;
    private NavMeshAgent Agent;
    private IsFoodClose Ifc;


    public GoToFood(GameObject creatureBody,IsFoodClose ifc)
    {
        this.CreatureBody = creatureBody;
        this.Agent = creatureBody.GetComponent<NavMeshAgent>();
        this.Ifc = ifc;
    }

    public override NodeState Evaluate()
    {
        PlantTarget = Ifc.GetPlantTarget();
        //problem
        try
        {
            Agent.destination = PlantTarget.transform.position;
            CreatureBody.GetComponent<DumbCreature>().CreatureBehavior = Creature.Behavior.GoingToFood;
            return NodeState.SUCCES;
        }
        catch
        {
            return NodeState.FAILURE;
        }
    }
    
    //other custom methods for fullfiling the Evaluate
}

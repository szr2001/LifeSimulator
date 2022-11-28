using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoToPack : Node
{

    //private variables
    private GameObject CreatureBody;
    private NavMeshAgent navMesh;
    private List<Collider> CreatureCollider = new List<Collider> ();
    private Vector3 PackLocation = new Vector3(-99, -99, -99);

    //constructor to set the private variables
    public GoToPack(GameObject Creature, List<Collider> CreatureCollider)
    {
        this.CreatureBody = Creature;
        this.navMesh = Creature.GetComponent<NavMeshAgent>();
        this.CreatureCollider = CreatureCollider;
    }
    //zii creaturii sa mearga la cea mai apropiata creatura
    public override NodeState Evaluate()
    {
        //Problem
        if (PackLocation == new Vector3(-99, -99, -99))
        {
            PackLocation = setClosestPack();
        }
        if(PackLocation == new Vector3(-99, -99, -99))
        {
            CreatureCollider.Clear ();
            return NodeState.FAILURE;
        }
        if (CreatureBody.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            CreatureBody.GetComponent<DumbCreature>().CreatureBehavior = Creature.Behavior.GoToPack;
            navMesh.speed = 4;
            navMesh.SetDestination(PackLocation);
            PackLocation = new Vector3(-99, -99, -99);
        }
        if (CreatureBody.transform.position == PackLocation)
        {
            PackLocation = new Vector3(-99, -99, -99);
            return NodeState.SUCCES;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
 
    //calculeaza cea mai apropiata creatura
     Vector3 setClosestPack()
    {
        Vector3 ClosestCrt = new Vector3(-99, -99, -99);
        float distance = 1000;
        if (CreatureCollider.Count == 0)
        {
            return new Vector3(-99, -99, -99);
        }
        if (CreatureCollider.Count == 1)
        {
            return CreatureCollider[0].gameObject.transform.position;
        }
        foreach(Collider c in CreatureCollider)
        {
            Vector3 Dist = CreatureBody.transform.position - c.transform.position;
            if(Dist.magnitude < distance)
            {
                distance = Dist.magnitude;
                ClosestCrt = c.gameObject.transform.position;
            }
        }
        return ClosestCrt;
    }
    //other custom methods for fullfiling the Evaluate
}


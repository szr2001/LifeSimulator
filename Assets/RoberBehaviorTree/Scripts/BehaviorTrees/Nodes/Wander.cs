using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wander : Node
{
    private GameObject CreatureBody;
    private int Walkrange = 5;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Vector3 GoToLocation = new Vector3(-99,-99,-99);
    public Wander(GameObject Creaturebody,int walkrange)
    {
        Walkrange = walkrange;
        CreatureBody = Creaturebody;
        navAgent = CreatureBody.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    //adauga o locatie noua unde sa se miste daca a ajuns dela la locatia veche
    public override NodeState Evaluate()
    {
        GoToLocation = new Vector3(-99, -99, -99);
        if (GoToLocation == new Vector3(-99, -99, -99))
        {
            GoToLocation = getRandomLocation();
        }
        if (CreatureBody.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            CreatureBody.GetComponent<DumbCreature>().CreatureBehavior = Creature.Behavior.Wander;
            navAgent.speed = 2;
            navAgent.SetDestination(GoToLocation);
        }
        if(CreatureBody.transform.position == GoToLocation)
        {
            GoToLocation = new Vector3(-99, -99, -99);
            return NodeState.SUCCES;
        }
        else
        {
            return NodeState.RUNNING;
        }
        
        
    }
    
    //i-a o locatie random in juru creaturii
    private Vector3 getRandomLocation()
    {
        //E gresit aici ceva la randomwalkrange position
        Vector3 creaturepos = CreatureBody.transform.position;
        Vector3 randomwalkrange = new Vector3(creaturepos.x + Random.Range(-Walkrange, Walkrange), CreatureBody.transform.position.y, creaturepos.z + Random.Range(-Walkrange, Walkrange));
        if(Physics.Linecast(randomwalkrange,randomwalkrange - new Vector3(0, 50, 0)))
        {
            return randomwalkrange;

        }
        else
        {
            return new Vector3(-99, -99, -99);
        }


    }



}

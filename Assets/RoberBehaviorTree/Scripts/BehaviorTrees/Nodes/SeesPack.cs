using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SeesPack : Node
{
    //Probleme la sees pack cred,se detecteaza pe el singur ca si pack si instant se completeaza ca el e deja la locatia lui
    //private variables
    private GameObject CreatureBody;
    private Collider[] hitColliders;
    private float DetectableRange;
    public List<Collider> CreatureCollider = new List<Collider>();

    public SeesPack(GameObject Creature,float detectableRange)
    {
        this.CreatureBody = Creature;
        this.DetectableRange = detectableRange;
    }


    //decide daca o creatura de aceasi specie e in raza vizuala
    public override NodeState Evaluate()
    {
        if (IsPackClose())
        {
            CreatureCollider.Remove(CreatureBody.GetComponent<CapsuleCollider>());
            return NodeState.SUCCES;
          
        }
        else
        {
           
            return NodeState.FAILURE;
        }
    }

    //calculeaza daca o creatura de aceasi specie e in raza vizuala
    public bool IsPackClose()
    {
        CreatureCollider.Clear();
        hitColliders = Physics.OverlapSphere(CreatureBody.transform.position, DetectableRange);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.CompareTag("DumbAnimal") == true && CreatureCollider.Count <= 30)
            {
                CreatureCollider.Add(c);
            }
        }
        if (CreatureCollider.Count > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}



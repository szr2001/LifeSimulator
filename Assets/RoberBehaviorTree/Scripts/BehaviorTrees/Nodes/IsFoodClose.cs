using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFoodClose : Node
{
    //private variables
    //MaxDetectableDistance trb sa fie 5-10-15 etc
    private float MaxDetectableDistance;
    private string FoodTag;
    private GameObject CreatureBody;
    private Collider[] hitColliders;
    private GameObject FoodTarget = null;


    public IsFoodClose(string foodTag,float maxdetectableDistance, GameObject creatureBody)
    {
        this.FoodTag = foodTag;
        this.MaxDetectableDistance = maxdetectableDistance;
        CreatureBody = creatureBody;
    }
    public GameObject GetPlantTarget()
    {
        if (FoodTarget != null)
        {
            return FoodTarget;
        }
        else
        {
            return null;
        }
    }
    public override NodeState Evaluate()
    {
        if (isFoodClose())
        {
            return NodeState.SUCCES;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
    public bool isFoodClose()
    {
        int smalldiss = 5;
        while(smalldiss < MaxDetectableDistance)
        {
            hitColliders = Physics.OverlapSphere(CreatureBody.transform.position, smalldiss);
            foreach (Collider c in hitColliders)
            {
                if (c.gameObject.CompareTag(FoodTag) && FoodTarget == null)
                {
                    FoodTarget = c.gameObject;
                    break;
                }
            }
            if (FoodTarget != null)
            {
                return true;
            }
            smalldiss += 5;
        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarFromPack : Node
{
    private float DetectableDistance;
    private float DefaultDetectableDistance;
    private GameObject CreatureBody;
    private Collider[] hitColliders;
    private List<Collider> CreatureCollider = new List<Collider> ();
    public FarFromPack(float detectabledistance, GameObject Creature)
    {
        this.CreatureBody = Creature;
        this.DetectableDistance = detectabledistance;
        this.DefaultDetectableDistance = detectabledistance;
    }

    public void ChangeDetectableDistance(float d)
    {
        DetectableDistance = d;
    }
    //alege daca go to pack sau wander
    public override NodeState Evaluate()
    {
        if (isfar())
        {
            //creaturebody change gotopackradious e doar ptr a vizualiza
            CreatureBody.GetComponent<DumbCreature>().ChangeGoToPackRadious(1);
            DetectableDistance = 1;
            return NodeState.SUCCES;
        }
        else
        {
            CreatureBody.GetComponent<DumbCreature>().ChangeGoToPackRadious(DefaultDetectableDistance);
            DetectableDistance = DefaultDetectableDistance;
            return NodeState.FAILURE;
        }
    }

    //calculeaza daca e singur
    public bool isfar()
    {
        CreatureCollider.Clear();
        hitColliders = Physics.OverlapSphere(CreatureBody.transform.position, DetectableDistance);
        foreach(Collider c in hitColliders)
        {
            if(c.gameObject.CompareTag("DumbAnimal") == true && CreatureCollider.Count <= 2)
            {
                CreatureCollider.Add(c);
            }
        }
        if (CreatureCollider.Count > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //other custom methods for fullfiling the Evaluate
}

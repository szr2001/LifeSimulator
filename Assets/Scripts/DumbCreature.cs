using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DumbCreature : Creature
{
    public GameObject Selected;

    private Selector TopNode;
    private float gizmoffpdistance = 8;


    public override IEnumerator SpecialAbility()
    {
        yield return new WaitForSeconds(0);
    }
    void Start()
    {
        StatsHandler.AddDumbAnimal(gameObject);
        CreateBehaviorTree();
        StartCoroutine(Aging());
        StartCoroutine(LoseHunger());
        StartCoroutine(UpdateBehaviorTree());
    }
     //creaza behavior tree
    void CreateBehaviorTree()
    {
        //Walk
        FarFromPack ffp = new FarFromPack(8, gameObject);
        SeesPack sp = new SeesPack(gameObject,30);
        GoToPack gtp = new GoToPack(gameObject, sp.CreatureCollider);
        Wander w = new Wander(gameObject, 3);
        Sequence DetectPack = new Sequence(new List<Node> { sp, gtp });
        Selector LookForPack = new Selector(new List<Node> { DetectPack, w });
        Sequence StayNearPack = new Sequence(new List<Node> { ffp, LookForPack });
        Selector WalkTopNode = new Selector(new List<Node> { StayNearPack, w });

        //eat
        IsFoodClose ifc = new IsFoodClose("Plant", 30, gameObject);
        GoToFood gtf = new GoToFood(gameObject, ifc);
        Eat e = new Eat(ifc, gameObject);
        Sequence DetectingFood = new Sequence(new List<Node> { ifc, gtf, e });
        Selector FindFood = new Selector(new List<Node> { DetectingFood, w });
        IsHungry ih = new IsHungry(gameObject, 70);
        Sequence EatTopNode = new Sequence(new List<Node> { ih, FindFood });

        TopNode = new Selector(new List<Node> { EatTopNode, WalkTopNode });
    }
    //schimba ghizmo radious o data cu far from pack acceptable radious
    public void ChangeGoToPackRadious(float r)
    {
        gizmoffpdistance = r;
    }

    void OnDrawGizmos()
    {
        //walk range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
        //Far From pack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoffpdistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 30);
    }
    public IEnumerator UpdateBehaviorTree()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(MindControlled == false)
            {
                TopNode.Evaluate();
            }
        }
    }   
}

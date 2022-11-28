using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Creature : MonoBehaviour
{
    public Genes CGenes = new Genes();
    public GameObject DropOnDeath;
    public GameObject Body;
    public Behavior CreatureBehavior;
    public int age = 0;
    public float hunger = 100f;
    public float health = 100f;
    public bool MindControlled = false;
    public enum Diet { ierbivore,carnivore };
    public enum Gender { male, female };
    public enum Behavior { Wander, GoToPack, LookingForFood,GoingToFood,Eating};
    public class Genes
    {
        public float threatScore;
        public float size = 0.2f;
        public float agresivity = 0f;
        public float damage = 0f;
        public float specialAbilityDuration = 0f;
        public float specialAbilitycost = 50f;
        public float agingTime = 60;
        public int SenseRange = 15;
        public float HungerloseTime = 3;
        public float hungerlose = 1f;
        public int oldage = 5;
        public int maxchildren = 3;
        public float speed = 0.2f;
        public Gender CreatureGender;
        public Diet CreatureDiet;
        static public Genes operator +(Genes g)
        {
            Genes RetGen = new Genes();
            return RetGen;
        }
    }
    public void Death()
    {
        GameObject meat = Instantiate(DropOnDeath);
        meat.GetComponent<FoodScript>().SetFood(30f); //Adauga metoda de calculat food
        meat.transform.position = Body.transform.position;
        StatsHandler.RemoveDumbAnimal(Body);
        Destroy(Body);
    }
    public void GetDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Death();
        }
    }
    public abstract IEnumerator SpecialAbility();
    public IEnumerator Aging()
    {
        while (age <= CGenes.oldage)
        {
            yield return new WaitForSeconds(CGenes.agingTime);
            age++;
            if (age == CGenes.oldage)
            {
                Death();
            }
        }
    }
    public IEnumerator LoseHunger()
    {
        while (hunger >= 0)
        {
            yield return new WaitForSeconds(CGenes.HungerloseTime);
            hunger -= CGenes.hungerlose;
            if (hunger <= 0)
            {
                Death();
            }
        }
    }
}



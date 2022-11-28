using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject Self;
    public Diet Food;

    private float size = 0.5f;
    private float maxsize = 6;
    [SerializeField]
    private float FoodAmount = 0;
    private int GrowthTime;
    private int Childs;
    private void Start()
    {

        if (Food == Diet.ierbivore)
        {
            Childs = Random.Range(1, 3);
            GrowthTime = Random.Range(10, 35);
            FoodAmount = Random.Range(3, 9);
            StatsHandler.IncrementPlants();
            StartCoroutine(Grow());
        }
        if(Food == Diet.carnivore)
        {
            StartCoroutine(Die(500,false));
        }
        RecalculateSize();
    }
    public void SetFood(float food)
    {
        FoodAmount = food;
        RecalculateSize();
    }
    public float Eat(float hunger) 
    {
        float foodneeded = 100 - hunger;
        if(foodneeded > FoodAmount)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(Die(0,false));
            return hunger += FoodAmount;
        }
        else
        {
            FoodAmount = FoodAmount - foodneeded;
            RecalculateSize();
            return 100;
        }
    }
    private void RecalculateSize()
    {
        float FoodSizeModifier = 0;
        switch (Food)
        {
            case Diet.carnivore:
                FoodSizeModifier = 10;
                break;
            case Diet.ierbivore:
                FoodSizeModifier = 3;
                break;
        }
        size = FoodAmount / FoodSizeModifier;
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
    private IEnumerator Grow()
    {
        while (true)
        {
            yield return new WaitForSeconds(GrowthTime);
            size *= 1.15f;
            gameObject.transform.localScale = new Vector3(size, size, size);
            FoodAmount *= 1.2f;
            if (size >= maxsize)
            {
                StartCoroutine(Die(0,true));
                yield break;
            }
        }


    }

    IEnumerator Die(float wait,bool dropSeeds)
    {
        yield return new WaitForSeconds(wait);
        if (Food == Diet.ierbivore && dropSeeds == true)
        {
            for (int i = 0; i < Childs; i++)
            {
                Vector3 BabyLocation = new Vector3(gameObject.transform.position.x + Random.Range(-1, 1), 0.25f, gameObject.transform.position.z + Random.Range(-1, 1));
                GameObject.Instantiate(Self, BabyLocation, new Quaternion(0, Random.Range(-180, 180), 0, 100));
            }
        }
        StatsHandler.DecrementPlants();
        Destroy(gameObject);
    }
    public enum Diet
    {
        ierbivore,
        carnivore
    }
    
    // Start is called before the first frame update

}

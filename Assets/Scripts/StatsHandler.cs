using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatsHandler 
{
   
    private static int PlantsCount = 0;
    private static int DumbCreaturesCount = 0;
    public static bool OverUi = false;

    public static void IncrementPlants()
    {
        PlantsCount += 1;
    }
    public static void DecrementPlants()
    {
        PlantsCount -= 1;
    }
    public static int ReadPlantsCount()
    {
        return PlantsCount;
    }
    public static int ReadDumbCreatureCount()
    {
        return DumbCreaturesCount;
    }
    public static void AddDumbAnimal(GameObject D)
    {
        DumbCreaturesCount = +1;
    }
    public static void RemoveDumbAnimal(GameObject D)
    {
        DumbCreaturesCount -= 1;
    }
    public static void OverUiTrue()
    {
        OverUi = true;
    }
    public static void OverUiFalse()
    {
        OverUi = false;
    }
}

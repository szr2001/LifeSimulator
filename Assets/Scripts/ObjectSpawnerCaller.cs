using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerCaller : MonoBehaviour
{

    public GameObject ObjSpawnerReff;
    public GameObject ObjectMenu;
    public GameObject SpawnObject;
    public int SpawnObjTrans;
    public string Objtag;
    public bool RandScaleOff;
    public void CallObjSpawner()
    {
        (GameObject g, int t, string st, bool sf) T = (SpawnObject, SpawnObjTrans,Objtag, RandScaleOff);
        ObjSpawnerReff.GetComponent<ObjectPaintBrush>().StartBrush(T);
    }
}

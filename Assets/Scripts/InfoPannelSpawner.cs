using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InfoPannelSpawner : MonoBehaviour
{
    public GameObject Pannel;

    private GameObject ActivePanel;
    private float offSet;
    public void CreatePannel((string name,string desc,Vector3 mousepos, bool isRight)PInfo)
    {
        if (ActivePanel != null)
        {
            RemovePanel();
        }
        ActivePanel = GameObject.Instantiate(Pannel);
        string pname = GetObjectByN("Name").GetComponent<Text>().text = PInfo.name;
        string pdesc = GetObjectByN("Desc").GetComponent<Text>().text = PInfo.desc;
        ActivePanel.transform.SetParent(gameObject.transform);
        offSet = pdesc.Length * 4f;
        if(offSet > 120)
        {
            offSet = 120;
        }
        if(offSet < 35)
        {
            offSet = 35;
        }
        if (PInfo.isRight)
        {
            ActivePanel.transform.position = PInfo.mousepos + new Vector3(offSet, 55, 0);
        }
        else
        {
            ActivePanel.transform.position = PInfo.mousepos + new Vector3(-offSet, 55, 0);
        }
    }
    public GameObject GetObjectByN(string name)
    {
        GameObject GReturn = null;
        foreach (Transform g in ActivePanel.transform.GetComponentsInChildren<Transform>())
        {
            if(g.gameObject.name == name)
            {
                GReturn = g.gameObject;
            }
        }
        return GReturn;
    }
    public void RemovePanel()
    {
        Destroy(ActivePanel);
        ActivePanel = null;
    }
}

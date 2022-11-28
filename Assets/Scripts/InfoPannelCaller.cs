using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InfoPannelCaller : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject InfoSpawnerReff;
    public string ObjectName;
    [TextArea]
    public string ObjectDesc;
    public bool IsRight;

    private (string name, string desc, Vector3 mousepos, bool isRight) PInfo;
    private void Start()
    {
        PInfo.name = ObjectName;
        PInfo.desc = ObjectDesc;
        PInfo.isRight = IsRight;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PInfo.mousepos = Input.mousePosition;
        InfoSpawnerReff.GetComponent<InfoPannelSpawner>().CreatePannel(PInfo);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InfoSpawnerReff.GetComponent<InfoPannelSpawner>().RemovePanel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverUiBool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StatsHandler.OverUiTrue();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StatsHandler.OverUiFalse();
    }
}

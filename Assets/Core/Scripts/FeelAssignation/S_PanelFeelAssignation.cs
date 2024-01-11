using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static S_FeelAssignationManager;

public class S_PanelFeelAssignation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [NonSerialized]
    public bool MouseHoverPanel = false;

    public delegate void MouseExitPanel();
    public event MouseExitPanel mouseExitPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseHoverPanel = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseHoverPanel = false;
        mouseExitPanel.Invoke();
    }
}

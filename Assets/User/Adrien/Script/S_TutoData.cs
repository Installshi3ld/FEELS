using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "SO_TutoData", menuName = "Data/TutoData", order = 1)]
public class S_TutoData : ScriptableObject
{
    public bool dataInfo;
    public bool dataBonus;
    public int OneTime;
    public Action dataInfoAction;
}

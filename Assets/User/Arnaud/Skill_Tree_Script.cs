using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Tree_Script : MonoBehaviour
{
    public string skillName;
    public Sprite skillSprite;

    [TextArea(1,3)]
    public string skillDes;
    public bool isUpgrade;
}

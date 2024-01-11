using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public Image skillImage;
    public TMP_Text skillNameText;
    public TMP_Text skillDesText;

    public int skillButtonId;

    public void pressSkillButton()
    {
        Skill_Manager.instance.activateSkill = transform.GetComponent<Skill_Tree_Script>();

        skillImage.sprite = Skill_Manager.instance.skills[skillButtonId].skillSprite;
        skillNameText.text = Skill_Manager.instance.skills[skillButtonId].skillName;
        skillDesText.text = Skill_Manager.instance.skills[skillButtonId].skillDes;

    }


}

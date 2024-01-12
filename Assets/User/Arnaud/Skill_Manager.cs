using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Manager : MonoBehaviour
{
    public static Skill_Manager instance;

    public Skill_Tree_Script[] skills;
    public SkillButton[] skillButtons;

    [SerializeField] public Skill_Tree_Script activateSkill;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }


}

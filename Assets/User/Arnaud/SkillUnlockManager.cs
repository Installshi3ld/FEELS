using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SkillUnlockManager : MonoBehaviour
{
    
    void UnlockSkill(S_SkillTreeUnlock skillUnlocked)
    {
        skillUnlocked.unlock = true;
    }

}

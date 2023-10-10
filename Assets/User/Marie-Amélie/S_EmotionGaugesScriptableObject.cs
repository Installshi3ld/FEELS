using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Player emotions")]
public class S_EmotionGaugesScriptableObject : ScriptableObject
{
    public float joy, anger, sadness, fear;

    public float currentMood;

    public float feels;

    private void OnEnable()
    {
        joy = 0;
        anger = 0;
        sadness = 0;
        fear = 0;
    }
}

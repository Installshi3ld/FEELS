using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TestButton : MonoBehaviour
{
    public S_EmotionScriptableObject emotion;

    public void TestFunction()
    {
        emotion.IncreaseEmotion(3);
    }
}

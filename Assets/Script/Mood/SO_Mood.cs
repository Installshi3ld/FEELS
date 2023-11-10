using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SO_MoodPercent", menuName = "Mood/Moods", order = 1)]
public class SO_Mood : ScriptableObject
{
    public float Joy, Angry, Sad, Fear;
    public bool isUnlocked;

    public Sprite moodImage;
    
    public List<float> GetMoodValueAsList()
    {
        return new List<float> {Joy, Angry, Sad, Fear };
    }

}

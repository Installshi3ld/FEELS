using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MoodUI : MonoBehaviour
{
    public Image MoodImage;
    public S_MoodManager Manager;

    private void OnEnable()
    {
        Manager.changeMoodImage += ChangeImage;
    }
    private void OnDisable()
    {
        Manager.changeMoodImage -= ChangeImage;
    }
    public void ChangeImage(SO_Mood mood)
    {
        MoodImage.GetComponent<Image>().sprite = mood.moodImage;
    }
}

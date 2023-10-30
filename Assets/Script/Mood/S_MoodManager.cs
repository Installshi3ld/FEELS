using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class S_MoodManager : MonoBehaviour
{
    public List<SO_Mood> moodList;
    
    public SO_MoodPercent JoyPercent, AngryPercent, SadPercent, FearPercent; 
    int currentSelectedMood = 0;

    public delegate void ChangeMoodImage(SO_Mood mood);
    public ChangeMoodImage changeMoodImage;
    public void SelectedNextMood()
    {
        do
        {
            currentSelectedMood++;
            if(currentSelectedMood >= moodList.Count)
            {
                currentSelectedMood = 0;
            }
        } while (!moodList[currentSelectedMood].isUnlocked );

        UpdatePercent();
        changeMoodImage(moodList[currentSelectedMood]);
    }

    public void SelectedPreviousMood()
    {
        do
        {
            currentSelectedMood--;
            if (currentSelectedMood < 0)
            {
                currentSelectedMood = moodList.Count - 1;
            }
        } while (!moodList[currentSelectedMood].isUnlocked);

        UpdatePercent();
        changeMoodImage(moodList[currentSelectedMood]);
    }

    void UpdatePercent()
    {
        JoyPercent.percent = moodList[currentSelectedMood].Joy;
        AngryPercent.percent = moodList[currentSelectedMood].Angry;
        SadPercent.percent = moodList[currentSelectedMood].Sad;
        FearPercent.percent = moodList[currentSelectedMood].Fear;
        print("<color=orange>Joy Percent = " + moodList[currentSelectedMood].Joy.ToString() +
            "<color=red> Angry Percent = " + moodList[currentSelectedMood].Angry.ToString() +
            "<color=#4DA1FF> Sad Percent = " + moodList[currentSelectedMood].Sad.ToString() +
            "<color=#FF6AFD> Fear Percent = " + moodList[currentSelectedMood].Fear.ToString() + "</color>");
    }

}

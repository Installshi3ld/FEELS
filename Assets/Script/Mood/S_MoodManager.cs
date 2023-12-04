using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class S_MoodManager : MonoBehaviour
{
    public List<SO_Mood> moodList;
    
    public SO_MoodPercent JoyPercent, AngryPercent, SadPercent, FearPercent; 
    public int currentSelectedMood = 0;

    public delegate void ChangeMoodImage(SO_Mood mood);
    public ChangeMoodImage changeMoodImage;
 
    public delegate void ChangeUIFeelsColor();
    public event ChangeUIFeelsColor OnChangeUIFeelsColor;   
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

        GetMoodHighestValue();

        OnChangeUIFeelsColor.Invoke();


    }
    float firstValue=-1, secondValue =-1;
    public List<float> GetMoodHighestValue()
    {
        
        List<float> moodsPercentValue = moodList[currentSelectedMood].GetMoodValueAsList();
        firstValue = moodsPercentValue.Max();
        moodsPercentValue.RemoveAt(moodsPercentValue.IndexOf(firstValue));
        secondValue = moodsPercentValue.Max();    
        
        return new List<float> { firstValue, secondValue };
    }
}

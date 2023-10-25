using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_LifeExperience : MonoBehaviour
{
    public GameObject fireLifeExperience;
    public GameObject wonderFireLifeExperience;
    [Space]
    List<GameObject> firelifeExperienceList = new List<GameObject>();
    public void AddFireLifeExperience() 
    {
        firelifeExperienceList.Add(GameObject.Instantiate(fireLifeExperience, new Vector3(0, -500, 0), Quaternion.identity));
    }

    public void RemoveFireLifeExperience()
    {
        foreach (GameObject lifeExperience in firelifeExperienceList)
        {
            lifeExperience.SetActive(false);
        }
        firelifeExperienceList.Clear();
    }

    
}

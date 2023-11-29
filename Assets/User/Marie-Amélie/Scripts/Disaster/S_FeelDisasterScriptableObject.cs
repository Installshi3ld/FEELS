using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new disaster", menuName = "Disaster/FeelDisaster")]
public class S_FeelDisasterScriptableObject : S_Disaster
{
    [SerializeField] private string description;

    [SerializeField] private List<S_Currencies> feelsToKill = new List<S_Currencies>();
    [SerializeField] private List<int> howMany = new List<int>();

    private void OnEnable()
    {
        Description = description;
    }
    public override void ProvoqueDisaster()
    {
        if(feelsToKill.Count > 0 && howMany.Count > 0 && feelsToKill.Count == howMany.Count)
        {
            for(int i = 0; i < feelsToKill.Count - 1; i++)
            {
                feelsToKill[i].amount -= howMany[i];
            }
        }
    }
}

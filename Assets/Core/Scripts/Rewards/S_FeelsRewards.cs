using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_FeelsReward", menuName = "Reward/Feels")]
public class S_FeelsRewards : S_Reward
{
    [SerializeField]
    private List<FeelsRewardMatch> rewards;
    public override void GetReward()
    {
        foreach (var reward in rewards)
        {
            reward.currencyType.AddAmount(reward.howManyForReward);
            Debug.Log("Have been rewarded with " + reward.howManyForReward);
        }
    }
}

[Serializable]
public struct FeelsRewardMatch
{
    public S_Currencies currencyType;
    public int howManyForReward;
}

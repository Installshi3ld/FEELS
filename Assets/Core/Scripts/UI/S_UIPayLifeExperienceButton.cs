using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_UIPayLifeExperienceButton : MonoBehaviour
{
    [SerializeField] private Button payLifeExpButton;

    [SerializeField] private S_UILifeExpDelegateScriptableObject LifeExpDelegate;

    [SerializeField] private S_Timeline timeLine;


    private void OnEnable()
    {
        payLifeExpButton.onClick.AddListener(OnPay);
        LifeExpDelegate.OnLifeExperienceSpawned += OnEnableLifeExperience;
        payLifeExpButton.gameObject.SetActive(false);
    }

    private void OnEnableLifeExperience(bool isLifeExperienceActive)
    {
        if (isLifeExperienceActive)
        {
            payLifeExpButton.gameObject.SetActive(true);
        }
        else
        {
            payLifeExpButton.gameObject.SetActive(false);
        }

    }
    private void OnPay()
    {
        timeLine.PayLifeExperience();
    }
}

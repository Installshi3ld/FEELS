using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_UIPayLifeExperienceButton : MonoBehaviour
{
    [SerializeField] private Button payLifeExpButton;

    [SerializeField] private S_Timeline timeLine;

    private void OnEnable()
    {
        payLifeExpButton.onClick.AddListener(OnPay);
    }

    private void OnPay()
    {
        timeLine.PayLifeExperience();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_DisasterUI : MonoBehaviour
{
    [SerializeField]
    private Image disasterImage;

    [SerializeField]
    private S_DisasterState disasterStateSo;

    private void OnEnable()
    {
        disasterStateSo.disasterEvent.AddListener(UpdateDisasterUI);
    }
    public void UpdateDisasterUI()
    {
        if(disasterStateSo.disasterState == true)
        {
            EnableVisualDisaster();
        }
        else
        {
            DisableVisualDisaster();
        }
    }
    private void EnableVisualDisaster()
    {
        disasterImage.gameObject.SetActive(false);
    }

    private void DisableVisualDisaster()
    {
        disasterImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        disasterStateSo.disasterEvent.Invoke();
    }
}

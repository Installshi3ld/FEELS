using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_FourToutAdrien : MonoBehaviour
{

    public TextMeshProUGUI titre;
    public TextMeshProUGUI requirement;
    //  public TextMeshProUGUI feelsNmb;

    public Toggle myToggle;

    private string title;
    public S_Requirement feelsRequirement;
    public S_ManageEvents manageEvent;
    public S_FeelsRequirement feelsRequirementNmb;

    public TextMeshProUGUI eventTitleAnim;
    public Image imageDisaster;

    public bool isRequirementComplete;

    public S_DisasterIsHere disasterIsHere1;
    public S_AnimationEventChange animationEventChange;
    public S_ImageAnimation animationImageDisaster;

    

    void Start()
    {
        InvokeRepeating("UpdateInfo", 0f, 60f);

        myToggle.isOn = isRequirementComplete;

        if (titre == null)
        {
            Debug.LogError("Veuillez assigner un objet TextMeshPro dans l'inspecteur Unity.");
        }
        ChangeText("Bonjour, monde !");

        if (requirement == null)
        {
            Debug.LogError("Veuillez assigner un objet TextMeshPro dans l'inspecteur Unity.");
        }
        ChangeText("Objectif 10 Feels de Joy");
        ToggleBoolean();


        feelsRequirement  = GetRequirement();

        if (feelsRequirement is S_FeelsRequirement)
        {
            S_FeelsRequirement feelsRequirementNmb = (S_FeelsRequirement)feelsRequirement;

            bool validé = feelsRequirementNmb.HasBeenFulfilled;
            Debug.Log("Validé l'info ?" + validé);
        }

    }

    private S_Requirement GetRequirement()
    {
        // Implémentez cette méthode pour renvoyer une instance appropriée
        return new S_FeelsRequirement();
    }

    private void Update()
    {
      

        if (feelsRequirement.HasBeenFulfilled == true)
        {
            Debug.Log("ConditionRemplie");
            isRequirementComplete = !isRequirementComplete;
            myToggle.isOn = isRequirementComplete;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            feelsRequirement = GetRequirement();
            feelsRequirement.CheckIsRequirementFulfilled();
            if (feelsRequirement is S_FeelsRequirement)
            {
                S_FeelsRequirement feelsRequirementNmb = (S_FeelsRequirement)feelsRequirement;

                bool validé = feelsRequirementNmb.HasBeenFulfilled;
                Debug.Log("Validé l'info ?" + validé);
            }

        }
    }

    public void ChangeText(string newText)
    {
        if (titre != null)
        {
            titre.text = newText;
            eventTitleAnim.text = newText;
            Debug.Log("Texte changé : " + newText);
        }
        else
        {
            Debug.LogError("Objet TextMeshPro non assigné. Assurez-vous de l'assigner dans l'inspecteur Unity.");
        }
    }

    public void ChangeTextDesc(string newText1)
    {
        if (requirement != null)
        {
            requirement.text = newText1;
            Debug.Log("Texte changé : " + newText1);
        }
        else
        {
            Debug.LogError("Objet TextMeshPro non assigné. Assurez-vous de l'assigner dans l'inspecteur Unity.");
        }
    }

    //  public void ChangeTextFeelsNmb(string newText2)
    // {
    //     if (feelsNmb != null)
    //     {
    //         feelsNmb.text = newText2;
    //         Debug.Log("Texte changé : " + newText2);
    //     }
    //     else
    //     {
    //         Debug.LogError("Objet TextMeshPro non assigné. Assurez-vous de l'assigner dans l'inspecteur Unity.");
    //     }
    // }

    public void ToggleBoolean()
    {
        isRequirementComplete = !isRequirementComplete;
        myToggle.isOn = isRequirementComplete;
    }

    public void UpdateInfo()
    {
            feelsRequirement = manageEvent.RequirementToReturn;

            


        if (isRequirementComplete= myToggle.isOn)
        {
            myToggle.isOn = false;
            
        }

        if (animationEventChange != null)
        {
            animationEventChange.FadeEventTitle();
        }
      
        if (animationImageDisaster != null)
        {
            animationImageDisaster.ImageScaleAndMove();
        }

        
        if( disasterIsHere1 != null)
        {
            disasterIsHere1.DisasterIsHere();
        }


        RefreshText();
        
      }
    
   
    public void RefreshText()
    {
        //ChangeTextFeelsNmb(feelsRequirementNmb.GetMyPrivateStringRequirNmb());

        ChangeText(feelsRequirement.GetMyPrivateString());
        Debug.Log("Texte actualisé !");
        ChangeTextDesc(feelsRequirement.GetMyPrivateStringDesc());
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_FourToutAdrien : MonoBehaviour
{

    public TextMeshProUGUI titre;
    public TextMeshProUGUI requirement;

    private string title;
    public S_Requirement feelsRequirement;
    public S_ManageEvents manageEvent;


    public TextMeshProUGUI eventTitleAnim;
    public Image imageDisaster;

    public S_DisasterIsHere disasterIsHere1;
    public S_AnimationEventChange animationEventChange;
    public S_ImageAnimation animationImageDisaster;



    void Start()
    {
        InvokeRepeating("UpdateInfo", 0f, 60f);

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


    public void UpdateInfo()
    {
            feelsRequirement = manageEvent.RequirementToReturn;

        
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
        

        ChangeText(feelsRequirement.GetMyPrivateString());
        Debug.Log("Texte actualisé !");
        ChangeTextDesc(feelsRequirement.GetMyPrivateStringDesc());
    }
}



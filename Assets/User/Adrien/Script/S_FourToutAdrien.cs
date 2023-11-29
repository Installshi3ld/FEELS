using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_FourToutAdrien : MonoBehaviour
{

    public TextMeshProUGUI titre;
    public TextMeshProUGUI requirement;

    private string title;
    public S_Requirement feelsRequirement;
    public S_ManageEvents manageEvent;


    void Start()
    {

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

    public void Update()
    {

       

        if (Input.GetKeyUp(KeyCode.Space)) 
        { 
            feelsRequirement = manageEvent.RequirementToReturn;
         
            RefreshText();
        }
      }
    

    public void RefreshText()
    {
        ChangeText(feelsRequirement.GetMyPrivateString());
        Debug.Log("Texte actualisé !");
        ChangeTextDesc(feelsRequirement.GetMyPrivateStringDesc());
    }

}



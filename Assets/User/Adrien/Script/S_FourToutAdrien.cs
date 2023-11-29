using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_FourToutAdrien : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro;
    private string title;
    public S_Requirement feelsRequirement;
    public S_ManageEvents manageEvent;


    void Start()
    {

        if (textMeshPro == null)
        {
            Debug.LogError("Veuillez assigner un objet TextMeshPro dans l'inspecteur Unity.");
        }
        ChangeText("Bonjour, monde !");
    }

    public void ChangeText(string newText)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = newText;
            Debug.Log("Texte changé : " + newText);
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
    }

}



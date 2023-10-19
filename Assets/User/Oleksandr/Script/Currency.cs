using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{

    public int Sadness;
    public int Joy;
    public int Fear;
    public int Anger;
    public int Tokens;

    public TextMeshProUGUI SadnessCount;
    public TextMeshProUGUI JoyCount;
    public TextMeshProUGUI FearCount;
    public TextMeshProUGUI TokensCount;
    public TextMeshProUGUI AngerCount;

    public Button SadnessIncrement;
    public Button JoyIncrement;
    public Button FearIncrement;
    public Button TokensIncrement;
    public Button AngerIncrement;

    // Start is called before the first frame update
    void Start()
    {

        SadnessCount.text = "0";
        JoyCount.text = "0";
        FearCount.text = "0";
        AngerCount.text = "0";
        TokensCount.text = "0";



        Sadness = 0; 
        Joy = 0;   
        Fear = 0;
        Anger = 0;
        Tokens = 0;
        SadnessIncrement.onClick.AddListener(SadnessPlus);
        JoyIncrement.onClick.AddListener(JoyPlus);
        FearIncrement.onClick.AddListener(FearPlus);
        TokensIncrement.onClick.AddListener(TokensPlus);
        AngerIncrement.onClick.AddListener(AngerPlus);

        

    }

    // Update is called once per frame
    void Update()
    {
        SadnessCount.text = Sadness.ToString();
        JoyCount.text = Joy.ToString();
        FearCount.text = Fear.ToString();
        TokensCount.text = Tokens.ToString();
        AngerCount.text = Anger.ToString();

        

    }

    void SadnessPlus() {
        Sadness++;   
    
    }

    void JoyPlus() {
        Joy++;
            
           }
    void FearPlus() {
        Fear++;

    }
    void TokensPlus()
    {
        Tokens++;
    }
    void AngerPlus()
    {
        Anger++;
    }
}

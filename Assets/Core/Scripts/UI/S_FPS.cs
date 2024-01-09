using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_FPS : MonoBehaviour
{
    public GameObject FpsContainer;
    public TextMeshProUGUI fpsValue, fpsText, T_AverageFps, T_AverageFpsText;

    int [] averageFpsList = new int [10000];
    int averageFpsIndex = 0;
    int averageFps = 0;

    bool bShowFps = true;
    private void Start()
    {
        ShowFPS();
        StartCoroutine(UpdateAverageFps());
    }
    
    
    void Update()
    {
        int Fps = ((int)(1 / Time.deltaTime));
        fpsValue.text = Fps.ToString();

        //Average FPS
        averageFpsList[averageFpsIndex] = Fps;
        if(averageFpsIndex + 1 >= 10000)
            averageFpsIndex = 0;
        else
            averageFpsIndex++;

        //SetColor
        if (Fps >= 50)
        {
            fpsValue.color = Color.green;
            fpsText.color = Color.green;
        }
        else
        {
            fpsValue.color = Color.red;
            fpsText.color = Color.red;

        }
        T_AverageFps.text = averageFps.ToString();
    }

    public void ShowFPS()
    {
        bShowFps = !bShowFps;
        FpsContainer.SetActive(bShowFps);
    }

    IEnumerator UpdateAverageFps()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            int tmpValue = 0;
            int index = 0;
            foreach (int i in averageFpsList)
            {
                if(i > 0)
                {
                    index++;
                    tmpValue += i;
                }
            }
            averageFps = tmpValue/index;

            


            //Set color
            if (averageFps >= 50)
            {
                T_AverageFps.color = Color.green;
                T_AverageFpsText.color = Color.green;
            }
            else
            {
                T_AverageFps.color = Color.red;
                T_AverageFpsText.color = Color.red;
            }

        }
        
    }
}

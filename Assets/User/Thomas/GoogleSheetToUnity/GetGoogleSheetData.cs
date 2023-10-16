using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GetGoogleSheetData : MonoBehaviour
{
    //https://sheets.googleapis.com/v4/spreadsheets/{SheetID}/values/{SheetName}?alt=json&key={APIKey}

    //https://sheets.googleapis.com/v4/spreadsheets/1SCV0KSFDv43Ju-laybj3DhIx9bNrsv_pOXKl9is59o8/values/test?alt=json&key=AIzaSyB3aEK_MB0SlLidbcfccoMvpLi6g3GmJiM

    List<List<string>> data;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObtainSheetData());

        
    }

    IEnumerator ObtainSheetData()
    {
        string json;
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1SCV0KSFDv43Ju-laybj3DhIx9bNrsv_pOXKl9is59o8/values/test?alt=json&key=AIzaSyB3aEK_MB0SlLidbcfccoMvpLi6g3GmJiM");
        yield return www.SendWebRequest();
        if(www.isNetworkError || www.isHttpError)
        {
            print("ERROR" + www.error);
        }
        else
        {
            json = www.downloadHandler.text;
            Debug.Log(json);

        }
    }
}

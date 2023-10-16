using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class GetGoogleSheetData : MonoBehaviour
{
    //https://sheets.googleapis.com/v4/spreadsheets/{SheetID}/values/{SheetName}?alt=json&key={APIKey}

    //https://sheets.googleapis.com/v4/spreadsheets/1SCV0KSFDv43Ju-laybj3DhIx9bNrsv_pOXKl9is59o8/values/test?alt=json&key=AIzaSyB3aEK_MB0SlLidbcfccoMvpLi6g3GmJiM


    //  regex =  \[([^\]\[]*)\]

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(ObtainSheetData());

        
    }

    List<List<string>> jsonElement = new List<List<string>>();
    IEnumerator ObtainSheetData()
    {
        string json;
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1SCV0KSFDv43Ju-laybj3DhIx9bNrsv_pOXKl9is59o8/values/test?alt=json&key=AIzaSyB3aEK_MB0SlLidbcfccoMvpLi6g3GmJiM");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            print("ERROR" + www.error);
        }
        else
        {
            json = www.downloadHandler.text;

            MatchCollection regexSort = Regex.Matches(json, "\\[([^\\]\\[]*)\\]");

            foreach (Match item in regexSort)
            {
                List<string> tmpList = new List<string>();

                string resultat = Regex.Replace(item.ToString(), @"[\n\[\]]|\s*[\""]*", "").Trim();
                print(resultat);
                //jsonElement[i].AddRange(resultat.Split(',').ToList());
            }

        }
    }
}

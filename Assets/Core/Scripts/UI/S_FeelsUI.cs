using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;
using Unity.VisualScripting;

public class S_FeelsUI : MonoBehaviour
{
    public S_Currencies joyFeels, angerFeels, fearFeels, sadnessFeels, currency;

    public TMP_Text jFeelsValue, aFeelsValue, fFeelsValue, sFeelsValue, t_Info, tokenValue;

    public Color infoTextColor;
    public Color increaseColor = Color.green;
    public Color decreaseColor = Color.red;

    public GameObject JFIncreasespriteObject, JFDecreasespriteObject, AFIncreasespriteObject, AFDecreasespriteObject, FFIncreasespriteObject, FFDecreasespriteObject, SFIncreasespriteObject, SFDecreasespriteObject;

    private float lastJoyFeelsAmount;
    private float lastAngerFeelsAmount;
    private float lastFearFeelsAmount;
    private float lastSadnessFeelsAmount;


    // Start is called before the first frame update
    void Start()
    {
        joyFeels.OnRefreshUi += RefreshUI;
        angerFeels.OnRefreshUi += RefreshUI;
        fearFeels.OnRefreshUi += RefreshUI;
        sadnessFeels.OnRefreshUi += RefreshUI;
        currency.OnRefreshUi += RefreshUI;
        RefreshUI();

        jFeelsValue.color = Color.white;
        aFeelsValue.color = Color.white;
        fFeelsValue.color = Color.white;
        sFeelsValue.color = Color.white;

        JFIncreasespriteObject.SetActive(false);
        JFDecreasespriteObject.SetActive(false);
        AFIncreasespriteObject.SetActive(false);
        AFDecreasespriteObject.SetActive(false);
        FFIncreasespriteObject.SetActive(false);
        FFDecreasespriteObject.SetActive(false);
        SFIncreasespriteObject.SetActive(false);
        SFDecreasespriteObject.SetActive(false);
    }

    public void RefreshUI()
    {
        // Check if joyFeels amount has increased or decreased
        if (joyFeels.Amount > lastJoyFeelsAmount)
        {
            StartCoroutine(JChangeColorCoroutine(increaseColor));
            StartCoroutine(JShowObjectForTime(JFIncreasespriteObject, 4.25f));
            JFDecreasespriteObject.SetActive(false);

        }
        else if (joyFeels.Amount < lastJoyFeelsAmount)
        {
            StartCoroutine(JChangeColorCoroutine(decreaseColor));
            StartCoroutine(JShowObjectForTime(JFDecreasespriteObject, 4.25f));
            JFIncreasespriteObject.SetActive(false);
        }

        // Check if angerFeels amount has increased or decreased
        if (angerFeels.Amount > lastAngerFeelsAmount)
        {
            StartCoroutine(AChangeColorCoroutine(increaseColor));
            StartCoroutine(AShowObjectForTime(AFIncreasespriteObject, 4.25f));
            AFDecreasespriteObject.SetActive(false);
        }
        else if (angerFeels.Amount < lastAngerFeelsAmount)
        {
            StartCoroutine(AChangeColorCoroutine(decreaseColor));
            StartCoroutine(AShowObjectForTime(AFDecreasespriteObject, 4.25f));
            AFIncreasespriteObject.SetActive(false);
        }

        // Check if fearFeels amount has increased or decreased
        if (fearFeels.Amount > lastFearFeelsAmount)
        {
            StartCoroutine(FChangeColorCoroutine(increaseColor));
            StartCoroutine(FShowObjectForTime(FFIncreasespriteObject, 4.25f));
            FFDecreasespriteObject.SetActive(false);
        }
        else if (fearFeels.Amount < lastFearFeelsAmount)
        {
            StartCoroutine(FChangeColorCoroutine(decreaseColor));
            StartCoroutine(FShowObjectForTime(FFDecreasespriteObject, 4.25f));
            FFIncreasespriteObject.SetActive(false);
        }

        // Check if sadnessFeels amount has increased or decreased
        if (sadnessFeels.Amount > lastSadnessFeelsAmount)
        {
            StartCoroutine(SChangeColorCoroutine(increaseColor));
            StartCoroutine(SShowObjectForTime(SFIncreasespriteObject, 4.25f));
            SFDecreasespriteObject.SetActive(false);
        }
        else if (sadnessFeels.Amount < lastSadnessFeelsAmount)
        {
            StartCoroutine(SChangeColorCoroutine(decreaseColor));
            StartCoroutine(SShowObjectForTime(SFDecreasespriteObject, 4.25f));
            SFIncreasespriteObject.SetActive(false);
        }


        // Update the UI text
        jFeelsValue.text = joyFeels.Amount.ToString();
        aFeelsValue.text = angerFeels.Amount.ToString();
        fFeelsValue.text = fearFeels.Amount.ToString();
        sFeelsValue.text = sadnessFeels.Amount.ToString();
        tokenValue.text = currency.Amount.ToString();

        // Update the lastJoyFeelsAmount for the next comparison
        lastJoyFeelsAmount = joyFeels.Amount;
        lastAngerFeelsAmount = angerFeels.Amount;
        lastFearFeelsAmount = fearFeels.Amount;
        lastSadnessFeelsAmount = sadnessFeels.Amount;
    }

    private IEnumerator JChangeColorCoroutine(Color targetColor)
    {
        jFeelsValue.color = targetColor;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Change the color back to the original color
        jFeelsValue.color = Color.white; // You can replace Color.white with your original color
    }
    private IEnumerator AChangeColorCoroutine(Color targetColor)
    {
        aFeelsValue.color = targetColor;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Change the color back to the original color
        aFeelsValue.color = Color.white; // You can replace Color.white with your original color
    }

    private IEnumerator FChangeColorCoroutine(Color targetColor)
    {
        fFeelsValue.color = targetColor;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Change the color back to the original color
        fFeelsValue.color = Color.white; // You can replace Color.white with your original color
    }

    private IEnumerator SChangeColorCoroutine(Color targetColor)
    {
        sFeelsValue.color = targetColor;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Change the color back to the original color
        sFeelsValue.color = Color.white; // You can replace Color.white with your original color
    }

    private IEnumerator JShowObjectForTime(GameObject obj, float time)
    {
        obj.SetActive(true);

        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Deactivate the object after the specified time
        obj.SetActive(false);
    }

    private IEnumerator AShowObjectForTime(GameObject obj, float time)
    {
        obj.SetActive(true);

        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Deactivate the object after the specified time
        obj.SetActive(false);
    }

    private IEnumerator FShowObjectForTime(GameObject obj, float time)
    {
        obj.SetActive(true);

        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Deactivate the object after the specified time
        obj.SetActive(false);
    }

    private IEnumerator SShowObjectForTime(GameObject obj, float time)
    {
        obj.SetActive(true);

        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Deactivate the object after the specified time
        obj.SetActive(false);
    }

    public void Info(string msg)
    {
        t_Info.text = msg;
        StartCoroutine("WaitForFadeOut");
    }

    float infoTextAlpha = 1f;
    bool canFade = false;
    IEnumerator WaitForFadeOut()
    {
        infoTextAlpha = 1f;
        canFade = false;
        t_Info.color = new Vector4(infoTextColor.r, infoTextColor.g, infoTextColor.b, infoTextAlpha);
        yield return new WaitForSeconds(1f);
        canFade = true;
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while (canFade)
        {
            yield return new WaitForSeconds(0.02f);
            infoTextAlpha -= 0.01f;
            t_Info.color = new Vector4(infoTextColor.r, infoTextColor.g, infoTextColor.b, infoTextAlpha);
            if (infoTextAlpha < 0)
                canFade = false;
        }
    }
}

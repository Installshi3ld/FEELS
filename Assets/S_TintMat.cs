using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class S_TintMat : MonoBehaviour
{
    [SerializeField]
    private float tintAmount;
    [SerializeField]
    private Material tintMaterial;

    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;

    [SerializeField]
    private float blinkSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMat());
    }

    // Update is called once per frame
    void Update()
    {
        tintAmount = Mathf.Lerp(tintAmount, 0, Time.deltaTime * blinkSpeed);
        tintAmount = Mathf.Clamp(tintAmount, 0, 1);

        tintMaterial.SetFloat("_TintAmount", tintAmount);
    }

    void ColorChange(Color color)
    {
        tintAmount += 1;
        tintMaterial.SetColor("_TintColor", color);
    }

    IEnumerator ChangeMat()
    {
        ColorChange(color1);
        yield return new WaitForSeconds(2f);
        ColorChange(color2);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChangeMat());
    }
}*/

public class S_TintMat : MonoBehaviour
{
    [SerializeField]
    private float tintAmount;
    [SerializeField]
    private Material tintMaterial;

    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;

    [SerializeField]
    private float blinkSpeed = 0.5f;

    private bool increasing = true; // Flag to control the direction of tintAmount change

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMat());
    }

    // Update is called once per frame
    void Update()
    {
        if (increasing)
        {
            tintAmount = Mathf.Lerp(tintAmount, 1f, Time.deltaTime * blinkSpeed);
            if (Mathf.Approximately(tintAmount, 1f))
                increasing = false;
        }
        else
        {
            tintAmount = Mathf.Lerp(tintAmount, 0.5f, Time.deltaTime * blinkSpeed);
            if (Mathf.Approximately(tintAmount, 0.5f))
                increasing = true;
        }

        tintMaterial.SetFloat("_TintAmount", tintAmount);
    }

    void ColorChange(Color color)
    {
        tintMaterial.SetColor("_TintColor", color);
    }

    IEnumerator ChangeMat()
    {
        ColorChange(color1);
        yield return new WaitForSeconds(2f);
        ColorChange(color2);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChangeMat());
    }
}

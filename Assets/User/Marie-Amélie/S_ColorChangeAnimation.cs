using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ColorChangeAnimation : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 2.0f;

    public Material material;

    private void Start()
    {
        StartCoroutine(ChangeColorOverTime());
    }

    private IEnumerator ChangeColorOverTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = endColor; // Ensure the final color is exactly the endColor
    }
}

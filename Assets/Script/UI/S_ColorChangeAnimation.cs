using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ColorChangeAnimation : MonoBehaviour
{
    public float duration = 2.0f;
    private Color startColor;

    public Material material;

    private void Start()
    {
        //StartCoroutine(ChangeColorOverTime(Color.blue));
    }

    private IEnumerator ChangeColorOverTime(Color targetedColor)
    {
        float elapsedTime = 0;
        startColor = material.color;

        while (elapsedTime < duration)
        {
            material.color = Color.Lerp(startColor, targetedColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = targetedColor; // Ensure the final color is exactly the endColor
    }
}

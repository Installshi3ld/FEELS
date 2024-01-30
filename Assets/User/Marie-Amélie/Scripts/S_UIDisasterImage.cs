using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UIDisasterImage : MonoBehaviour
{
    public Image image;
    public float blinkDuration = 3f;
    public float blinkSpeed = 1.5f;

    private void OnEnable()
    {
        image.enabled = false;
  //      S_Timeline.OnDisasterOccuring += MakeBlink;
    }

    private void OnDisable()
    {
 //       S_Timeline.OnDisasterOccuring -= MakeBlink;
    }

    private void MakeBlink(S_Requirement currentEvent)
    {
        if (image == null)
        {
            Debug.LogError("Image component is not assigned!");
            return;
        }

        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        image.enabled = true;

        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < blinkDuration)
        {
            // Calculate the alpha value using Lerp
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, Mathf.PingPong(elapsedTime * blinkSpeed, 1f));

            // Set the new color with updated alpha
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        image.enabled = false;
    }
}

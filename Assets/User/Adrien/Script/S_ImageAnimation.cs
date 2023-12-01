using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class S_ImageAnimation : MonoBehaviour
{
    public Image image; 


    public void Start()
    {
        // Set initial scale to zero
        Vector3 initialScale = new Vector3(1f, 1f, 1f); // Adjust as needed
        image.rectTransform.localScale = initialScale;

        // Animation of appearance and scaling
        image.rectTransform.DOScale(new Vector3(2f, 2f, 2f), 2.0f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Animation of shrinkage
                image.rectTransform.DOScale(initialScale, 2.0f) // Return to the initial scale
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        // Animation of movement to a new position
                        image.transform.DOMove(new Vector3(550f, 920f, 0f), 1.0f)
                                .SetEase(Ease.Linear)
                                .OnComplete(() =>
                                {
                                    // Animation completed, do something if needed
                                    Debug.Log("Animation completed!");
                                });
                    });
            });
    }

    public void ImageScaleAndMove()
    {
        // Set initial scale to zero
        Vector3 initialScale = new Vector3(1f, 1f, 1f); // Adjust as needed
        image.rectTransform.localScale = initialScale;

        // Animation of appearance and scaling
        image.rectTransform.DOScale(new Vector3(2f, 2f, 2f), 2.0f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Animation of shrinkage
                image.rectTransform.DOScale(initialScale, 2.0f) // Return to the initial scale
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        // Animation of movement to a new position
                        image.transform.DOMove(new Vector3(550f, 920f, 0f), 1.0f)
                                .SetEase(Ease.Linear)
                                .OnComplete(() =>
                                 {
                                        // Animation completed, do something if needed
                                         Debug.Log("Animation completed!");
                                 });        
                     });
            });
    }
}

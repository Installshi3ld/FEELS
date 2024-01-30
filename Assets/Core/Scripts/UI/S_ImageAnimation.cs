using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class S_ImageAnimation : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private RectTransform targetedPosition;

    private Vector3 initialPosition;

    private Vector3 GetImagePosition()
    {
        float sliderRightX = targetedPosition.position.x + targetedPosition.rect.width/2 - image.rectTransform.rect.width;
        Vector3 imagePosition = new Vector3(sliderRightX, targetedPosition.position.y, targetedPosition.position.z);

        return imagePosition;
    }

    private void Awake()
    {
        initialPosition = image.transform.position;
        image.gameObject.SetActive(false);
    }

  // private void OnEnable()
  // {
  //     S_Timeline.OnDisasterOccuring += ImageScaleAndMove;
  // }
  //
  // private void OnDisable()
  // {
  //     S_Timeline.OnDisasterOccuring -= ImageScaleAndMove;
  // }

    private IEnumerator OnAnimationEnded()
    {
        yield return new WaitForSeconds(1.5f);
        image.gameObject.SetActive(false);
        image.transform.position = initialPosition;
    }
    public void ImageScaleAndMove(S_Requirement currentR)
    {
        image.gameObject.SetActive(true);
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
                        image.transform.DOMove(GetImagePosition(), 1.0f) //Get dynamically the position to put the image (at the right of the slider, responsive)
                                .SetEase(Ease.Linear)
                                .OnComplete(() =>
                                {
                                    // Animation completed, do something if needed
                                    StartCoroutine(OnAnimationEnded());
                                });
                    });
            });
    }
}

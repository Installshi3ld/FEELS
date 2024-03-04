using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class S_ImageAnimation : MonoBehaviour
{
    [SerializeField]
    private Image imageSpot;

    [SerializeField]
    private RectTransform targetedPosition;

    private Vector3 initialPosition;

    private Vector3 GetImagePosition(S_Requirement currentR)
    {
        float sliderRightX = targetedPosition.position.x + targetedPosition.rect.width/2 - imageSpot.rectTransform.rect.width;
        Vector3 imagePosition = new Vector3(sliderRightX, targetedPosition.position.y, targetedPosition.position.z);

        return imagePosition;
    }

    private void Awake()
    {
        imageSpot.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        S_Timeline.OnPickedRequirement += ImageScaleAndMove;
    }

    private void OnDisable()
    {
        S_Timeline.OnPickedRequirement -= ImageScaleAndMove;
    }

    private IEnumerator OnAnimationEnded(S_Requirement currentR)
    {
        yield return new WaitForSeconds(1.5f);
        imageSpot.gameObject.SetActive(false);
        imageSpot.transform.position = initialPosition;
    }
    public void ImageScaleAndMove(S_Requirement currentR)
    {
        initialPosition = imageSpot.transform.position;
        imageSpot.gameObject.SetActive(true);

        //ici currentR est null
        if(currentR.spriteImage != null && imageSpot != null) { imageSpot.sprite = currentR.spriteImage; }

        // Set initial scale to zero
        Vector3 initialScale = new Vector3(1f, 1f, 1f); // Adjust as needed
        imageSpot.rectTransform.localScale = initialScale;

        // Animation of appearance and scaling
        imageSpot.rectTransform.DOScale(new Vector3(2f, 2f, 2f), 2.0f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Animation of shrinkage
                imageSpot.rectTransform.DOScale(initialScale, 2.0f) // Return to the initial scale
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        // Animation of movement to a new position
                        imageSpot.transform.DOMove(GetImagePosition(currentR), 1.0f) //Get dynamically the position to put the image (at the right of the slider, responsive)
                                .SetEase(Ease.Linear)
                                .OnComplete(() =>
                                {
                                    // Animation completed, do something if needed
                                    StartCoroutine(OnAnimationEnded(currentR));
                                });
                    });
            });
    }
}

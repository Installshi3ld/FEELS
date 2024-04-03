using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class S_EndRoundButton : MonoBehaviour
{
    public float shakeDuration = 3.5f;
    public float shakeStrength = 6.2f;
    public int shakeVibrato = 10;
    public float shakeRandomness = 90f;

    public float scaleMultiplier = 2f;
    public float scaleDuration = 0.2f;

    private float scaleOrigin = 1f;

    public float moveDistance = 0.2f;
    public float moveDuration = 0.2f;

    public S_Currencies _Currencies;
    public GameObject shakeObject;
    public void Shake()
    {
       ShakeAndEnlarge();
        // Utilisez DOTween pour faire trembler le GameObject
      //  shakeObject.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
    }

    public void ShakeAndEnlarge()
    {
        // Obtenez la position initiale du GameObject
        Vector3 initialPosition = shakeObject.transform.position;

        // Commencez par une séquence de tweens pour combiner le shake et le grossissement
        Sequence sequence = DOTween.Sequence();

        // Ajoutez le tween de déplacement vers le haut et la gauche
        sequence.Append(shakeObject.transform.DOMove(initialPosition + new Vector3(-moveDistance, moveDistance, 0f), moveDuration).SetEase(Ease.InOutQuad));


        // Ajoutez le tween de grossissement
        sequence.Append(shakeObject.transform.DOScale(shakeObject.transform.localScale * scaleMultiplier, scaleDuration));

        // Ajoutez le tween de shake
        sequence.Append(shakeObject.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness));

        // Ajoutez le tween de retour à la position initiale
        sequence.Append(shakeObject.transform.DOMove(initialPosition, moveDuration).SetEase(Ease.InOutQuad));

        // Ajoutez le tween de retour à la taille normale
        sequence.Append(shakeObject.transform.DOScale(shakeObject.transform.localScale / scaleOrigin , scaleDuration));

        // Lancez la séquence
        sequence.Play();

    }
}

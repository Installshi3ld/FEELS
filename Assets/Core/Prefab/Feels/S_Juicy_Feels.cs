using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class S_Juicy_Feels : MonoBehaviour
{
    // public float scaleYTarget = 1.5f; // L'�chelle finale sur l'axe Y
    // public float animationDuration = 1f; // Dur�e de l'animation
    // public Ease easeType = Ease.OutQuad; // Type d'interpolation
    //
    // void Start()
    // {
    //     // Animation de rescale sur l'axe Y
    //     transform.DOScaleY(scaleYTarget, animationDuration)
    //         .SetEase(easeType)
    //         .SetLoops(-1, LoopType.Yoyo); // boucle infinie
    // }

    // public float swingAngle = 30f; // Angle de balancement du pendule
    // public float swingDuration = 1f; // Dur�e d'une oscillation compl�te
    // public Ease easeType = Ease.Linear; // Type d'interpolation

    // void Start()
    // {
    //     // Animation de rotation du pendule sur l'axe X
    //     transform.DORotate(new Vector3(swingAngle, 0f, 0f), swingDuration)
    //         .SetEase(easeType)
    //         .SetLoops(-1, LoopType.Yoyo); // Boucle infinie avec l'effet yoyo
    // }

    //void Start()
    //{
    //   // Animation de rotation du pendule sur l'axe X
    //   transform.DOShakeScale(10f, new Vector3(1f, 1f, 1f), 10, 90f, true)
    //        .SetEase(easeType)
    //        .SetLoops(-1, LoopType.Yoyo); // Boucle infinie avec l'effet yoyo
    //}



    //public float swingAngle = 30f; // Angle de balancement
    //public float swingDuration = 1f; // Dur�e d'une oscillation compl�te
    //public Ease easeType = Ease.Linear; // Type d'interpolation
    //
    //void Start()
    //{
    //    // Animation de rotation pour le mouvement de balancier sur l'axe X seulement
    //    transform.DOLocalRotate(new Vector3(swingAngle, 0f, 0f), swingDuration)
    //        .SetEase(easeType)
    //        .SetLoops(-1, LoopType.Yoyo); // Boucle infinie avec l'effet yoyo
    //}

    // 
    // public float fanDistance = 1f; // Distance de d�placement
    // public float fanDuration = 1f; // Dur�e d'une oscillation compl�te
    // public Ease easeType = Ease.Linear; // Type d'interpolation
    //
    // void Start()
    // {
    //     // Animation de translation pour le mouvement en �ventail
    //     transform.DOMoveX(transform.position.x + fanDistance, fanDuration / 2f)
    //         .SetEase(easeType)
    //         .SetLoops(-1, LoopType.Yoyo); // Boucle infinie avec l'effet yoyo
    // }






    private Vector3 initialScale;
    private NavMeshAgent navMeshAgent;
    public float triggerSpeed = 1f; // Seuil de vitesse pour d�clencher la s�quence

    void Start()
    {
        initialScale = transform.localScale;
        navMeshAgent = GetComponent<NavMeshAgent>();

        // V�rifier la vitesse du NavMeshAgent � intervalles r�guliers
        InvokeRepeating("CheckSpeed", 0f, 0.1f);
    }

    void CheckSpeed()
    {
        // V�rifier si la vitesse d�passe le seuil
        if (navMeshAgent.velocity.magnitude > triggerSpeed)
        {
            StartScalingSequence();
        }
        else
        {
            RerurnToPrefabScale();
        }
    }

    void StartScalingSequence()
    {
        Sequence sequence = DOTween.Sequence();

    //    sequence.Append(transform.DOScale(initialScale, 0.20f));

        // Animation de rescale sur l'axe Y
        sequence.Append(transform.DOScaleY(initialScale.y + 0.25f, 0.10f));

        // Animation de retour � l'�chelle initiale sur l'axe Y
        sequence.Append(transform.DOScale(initialScale, 0.10f));

        // Animation de rescale sur l'axe X
        sequence.Append(transform.DOScaleX(initialScale.x + 0.75f, 0.10f));

        // Animation de retour � l'�chelle initiale sur l'axe X
        sequence.Append(transform.DOScale(initialScale, 0.10f));

        // Boucler la s�quence en ping-pong (avant-arri�re)
      //  sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.SetLoops(-1);
    }

    void RerurnToPrefabScale()
    {
        transform.DOScale(initialScale, 0.15f);
    }
}

    



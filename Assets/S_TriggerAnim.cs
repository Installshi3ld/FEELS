using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TriggerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Animator animator;

    void Start()
    {
        // Récupérer le composant Animator
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetTrigger("Trigger01");
        }
    }
}



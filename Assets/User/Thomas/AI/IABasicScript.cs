using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABasicScript : MonoBehaviour
{
    public Transform cible; // Le point B o� l'objet doit se d�placer

    private NavMeshAgent agent; // Le composant NavMeshAgent pour le d�placement

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("Le composant NavMeshAgent est manquant sur cet objet.");
        }
    }

    void Update()
    {
        // D�place l'objet vers le point B en utilisant le pathfinding
        if (cible != null && agent != null)
        {
            agent.SetDestination(cible.position);
        }
    }
}

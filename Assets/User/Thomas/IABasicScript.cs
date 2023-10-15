using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABasicScript : MonoBehaviour
{
    public Transform cible; // Le point B où l'objet doit se déplacer

    private NavMeshAgent agent; // Le composant NavMeshAgent pour le déplacement

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
        // Déplace l'objet vers le point B en utilisant le pathfinding
        if (cible != null && agent != null)
        {
            agent.SetDestination(cible.position);
        }
    }
}

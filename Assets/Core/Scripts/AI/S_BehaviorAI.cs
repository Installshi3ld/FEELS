using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.IO.Compression;

public class S_BehaviorAI : MonoBehaviour
{
    [SerializeField]
    private float walkRange;
    [SerializeField]
    private NavMeshAgent agent;

    private Vector3 destination;
    private bool walkPointSet = false;

    private void Update()
    {
        MovingAround();
    }
    private void MovingAround()
    {
        if (!walkPointSet) SearchForDestination();
        if (walkPointSet) agent.SetDestination(destination);
        if (Vector3.Distance(transform.position, destination) < 10) walkPointSet = false;
    }

    private void SearchForDestination()
    {
        float z = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destination = new Vector3(transform.position.x + x, 0, transform.position.z + z);

        walkPointSet = true;
    }

}

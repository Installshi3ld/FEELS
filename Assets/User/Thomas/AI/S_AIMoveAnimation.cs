using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_AIMoveAnimation : MonoBehaviour
{
    NavMeshAgent m_Agent;
    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    float lerpFactor = 0f;
    public float lerpSpeed = 0.5f;
    public float rotationZStart = -5f;
    public float rotationZEnd = 5f;
    // Update is called once per frame
    void Update()
    {
        /*
        if (m_Agent.velocity != Vector3.zero)
        {
            lerpFactor = Mathf.PingPong(Time.time * lerpSpeed, 1f);
            float targetRotationZ = Mathf.Lerp(rotationZStart, rotationZEnd, lerpFactor);

            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, targetRotationZ);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_AI : MonoBehaviour
{
    NavMeshAgent m_Agent;
    public FeelType m_FeelType;
    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    bool haveTask = false;
    // Update is called once per frame
    void Update()
    {
        if(!haveTask && m_Agent.velocity == Vector3.zero)
        {
            StartCoroutine(WaitForNextMovement());
        }
    }

    IEnumerator WaitForNextMovement()
    {
        haveTask = true;
        yield return new WaitForSeconds(3f);
        GetNewPositionToGoOn();
        haveTask = false;
    }
    void GetNewPositionToGoOn()
    {
        float xCoord = Random.Range(this.gameObject.transform.position.x - 30, this.gameObject.transform.position.x + 30);
        float zCoord = Random.Range(this.gameObject.transform.position.z - 30, this.gameObject.transform.position.z + 30);
        m_Agent.destination = new Vector3(xCoord, 0, zCoord);
    }
}

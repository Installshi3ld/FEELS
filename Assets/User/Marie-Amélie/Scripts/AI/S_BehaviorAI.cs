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

    private S_AIManager myAIManager;
    private FeelsMatchSprite myMatchSprite;

    private Vector3 destination;
    private bool walkPointSet = false;

    private void Start()
    {
        StartCoroutine(EndingMyOwnSuffering());
		StartCoroutine(DoFadeInAndOut());
    }
	
	private IEnumerator DoFadeInAndOut()
	{
		var renderer = gameObject.GetComponent<SpriteRenderer>();
		var color = renderer.color;
		color.a = 0f;
		renderer.color = color;

		renderer.DOFade(1.0f, 2.5f);
		yield return new WaitForSeconds(2.5f);
		renderer.DOFade(0f, 2.5f);
	}

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

    IEnumerator EndingMyOwnSuffering()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    public void Toto(S_AIManager manager, FeelsMatchSprite identity)
    {
        myAIManager = manager;
        myMatchSprite = identity;
    }

    private void OmgIAmDyingFromCringe()
    {
        myAIManager.IAmDead(myMatchSprite);
    }

    private void OnDestroy()
    {
        OmgIAmDyingFromCringe();
    }
}
